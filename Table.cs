using System.Collections.Generic;
using System.Linq;

namespace KSPE3Lib
{
    public class Table
    {
        private TablePageTemplate firstPageTemplate;
        private TablePageTemplate subsequentPageTemplate;
        private LineTemplate headerLineTemplate;
        private LineTemplate lineTemplate;
        private StringSeparator separator;
        private List<string> headerLineTexts;
        private E3Project project;
        private Graphic graphic;
        private E3Text text;
        private Group group;
        private Sheet currentSheet;
        private int sheetCount;
        private double y, x, uttermostPositionByY, bottomLineWidth;
        private List<int> sheetIds;

        public List<int> SheetIds
        {
            get
            {
                return sheetIds;
            }
        }

        public Table(E3Project project, LineTemplate headerLineTemplate, List<string> headerLineTexts, LineTemplate lineTemplate, double bottomLineWidth, StringSeparator separator, TablePageTemplate firstSheetTemplate) : this (project, headerLineTemplate,headerLineTexts, lineTemplate, bottomLineWidth, separator, firstSheetTemplate, null)
        {
        }

        public Table(E3Project project, LineTemplate headerLineTemplate, List<string> headerLineTexts, LineTemplate lineTemplate, double bottomLineWidth, StringSeparator separator, TablePageTemplate firstPagetTemplate, TablePageTemplate subsequentPageTemplate = null )
        {
            this.project = project;
            this.firstPageTemplate = firstPagetTemplate;
            if (subsequentPageTemplate == null)
                this.subsequentPageTemplate = firstPagetTemplate;
            else
                this.subsequentPageTemplate = subsequentPageTemplate;
            this.headerLineTemplate = headerLineTemplate;
            this.bottomLineWidth = bottomLineWidth;
            this.headerLineTexts = headerLineTexts;
            this.lineTemplate = lineTemplate;
            this.separator = separator;
            this.graphic = project.GetGraphicById(0);
            this.group = project.GetGroupById(0);
            this.text = project.GetTextById(0);
            currentSheet = project.GetSheetById(0);
            sheetCount = 0;
            sheetIds = new List<int>();
        }

        public void AddLine(List<string> texts)
        {
            if (currentSheet.Id == 0)
                CreateNewPage(firstPageTemplate);
            List<List<string>> separatedTexts = GetSeparatedTexts(texts);
            double lineHeight = GetLineHeight(separatedTexts);
            if (currentSheet.IsUnderTarget(uttermostPositionByY, currentSheet.MoveDown(y, lineHeight)))
            {
                CreateBottomLine(x, currentSheet.MoveRight(x, lineTemplate.Width), y);
                CreateNewPage(subsequentPageTemplate);
            }
            CreateLine(separatedTexts, x, y, lineHeight);
            y = currentSheet.MoveDown(y, lineHeight);
        }

        public void AddFinalGraphicLine()
        {
            CreateBottomLine(x, currentSheet.MoveRight(x, lineTemplate.Width), y);
        }

        private void CreateNewPage(TablePageTemplate pageTemplate)
        {
            CreateNewSheet(pageTemplate);
            currentSheet.Name = sheetCount.ToString();
            sheetIds.Add(currentSheet.Id);
            CreateHeader(pageTemplate);
            uttermostPositionByY = pageTemplate.UttermostPositionByY;
            y = currentSheet.MoveDown(pageTemplate.HeaderLineY, headerLineTemplate.Height);
            x = pageTemplate.HeaderLineX;
        }

        private int CreateNewSheet(TablePageTemplate template)
        {
            sheetCount++;
            int sheetId;
            if (sheetIds.Count > 0)
                sheetId = sheetIds[sheetIds.Count - 1];
            else
                sheetId = 0;
            return currentSheet.Create(sheetCount.ToString(), template.Format, sheetId, Position.After);
        }

        private int CreateHeader(TablePageTemplate sheetTemplate)
        {
            List<int> lineElementIds = CreateHeaderLine(sheetTemplate);
            int headerTextId = CreateHeaderText(sheetTemplate);
            if (headerTextId > 0)
                lineElementIds.Add(headerTextId);
            return group.CreateGroup(lineElementIds);
        }

        private List<int> CreateHeaderLine(TablePageTemplate sheetTemplate)
        {
            double yTop = sheetTemplate.HeaderLineY;
            double xLeft = sheetTemplate.HeaderLineX;
            List<int> ids = CreateLineGraphics(sheetTemplate.HeaderLineX, sheetTemplate.HeaderLineY, headerLineTemplate);
            double yText = currentSheet.MoveDown(yTop,headerLineTemplate.TextVerticalOffset);
            for (int i = 0; i < headerLineTexts.Count; i++)
            {
                double xText = currentSheet.MoveRight(xLeft, headerLineTemplate.TextHorizontalOffsets[i]);
                ids.Add( text.CreateText(currentSheet.Id, headerLineTexts[i], xText, yText, headerLineTemplate.Font));
            }
            return ids;
        }

        private int CreateHeaderText(TablePageTemplate sheetTemplate)
        {
            if (sheetTemplate.HeaderText == null)
                return -1;
            double xText = currentSheet.MoveRight(sheetTemplate.HeaderLineX, headerLineTemplate.Width/2);
            double yText = currentSheet.MoveUp(sheetTemplate.HeaderLineY, sheetTemplate.HeaderText.Offset);
            return text.CreateText(currentSheet.Id, sheetTemplate.HeaderText.Text, xText, yText, sheetTemplate.HeaderText.Font);
        }

        private List<List<string>> GetSeparatedTexts(List<string> texts)
        {
            List<List<string>> separatedTexts = new List<List<string>>(texts.Count);
            if (texts.Count <= lineTemplate.ColumnWidths.Count)
                for (int i = 0; i < texts.Count; i++)
                    separatedTexts.Add(separator.GetSeparatedStrings(texts[i], lineTemplate.ColumnWidths[i]));
            return separatedTexts;
        }

        private double GetLineHeight(List<List<string>> separatedTexts)
        {
            int maxCount = separatedTexts.Max(l => l.Count);    // максимальное количество перенесенных строк
            return lineTemplate.Height * maxCount;
        }

        private int CreateLine(List<List<string>> separatedTexts, double xLeft, double yTop, double lineHeight)
        {
            List<int> graphicIds = CreateLineGraphics(xLeft, yTop, lineTemplate, lineHeight);
            List<int> textids = CreateLineTexts(separatedTexts, xLeft, yTop);
            graphicIds.AddRange(textids);
            return group.CreateGroup(graphicIds);
        }

        private List<int> CreateLineGraphics(double xLeft, double yTop, LineTemplate template, double lineHeight = 0 )
        {
            if (lineHeight == 0)
                lineHeight = template.Height;
            double yBottom = currentSheet.MoveDown(yTop, lineHeight);
            double xRight = currentSheet.MoveRight(xLeft, template.Width);
            List<int> ids = new List<int>();
            ids.Add(graphic.CreateLine(currentSheet.Id, xLeft, yTop, xRight, yTop, template.HorizontalLineWidth)); // верхняя горизонтальная линия 
            ids.Add(graphic.CreateLine(currentSheet.Id, xLeft, yBottom, xRight, yBottom, template.HorizontalLineWidth)); // нижняя горизонтальная линия
            double xLine = xLeft;
            foreach (double width in template.ColumnWidths)
            {
                ids.Add(graphic.CreateLine(currentSheet.Id, xLine, yTop, xLine, yBottom, template.VerticalLineWidth)); // вертикальная линия
                xLine = currentSheet.MoveRight(xLine, width);
            }
            ids.Add(graphic.CreateLine(currentSheet.Id, xLine, yTop, xLine, yBottom, template.VerticalLineWidth)); // самая правая вертикальная линия
            return ids;
        }

        private List<int> CreateLineTexts(List<List<string>> separatedTexts, double xLeft, double yTop)
        {
            List<int> ids = new List<int>();
            for (int i = 0; i < separatedTexts.Count; i++)
            {
                double xText = currentSheet.MoveRight(xLeft, lineTemplate.TextHorizontalOffsets[i]);
                double yText = currentSheet.MoveDown(yTop, lineTemplate.TextVerticalOffset);
                foreach (string separatedText in separatedTexts[i])
                {
                    ids.Add(text.CreateText(currentSheet.Id, separatedText, xText, yText, lineTemplate.Font));
                    yText = currentSheet.MoveDown(yText, lineTemplate.Height);
                }
            }
            return ids;
        }

        private int CreateBottomLine(double xLeft, double xRight, double yLine)
        {
            return graphic.CreateLine(currentSheet.Id, xLeft, yLine, xRight, yLine, bottomLineWidth);
        }

    }
}
