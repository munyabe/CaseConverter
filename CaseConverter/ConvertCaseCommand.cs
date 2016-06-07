using System;
using EnvDTE;
using Microsoft.VisualStudio.Shell;

namespace CaseConverter
{
    /// <summary>
    /// 文字列をスネークケース⇒キャメルケース⇒パスカルケースの順に変換するコマンドです。
    /// </summary>
    internal sealed class ConvertCaseCommand : CommandBase
    {
        /// <summary>
        /// コマンドのIDです。
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// コマンドメニューグループのIDです。
        /// </summary>
        public static readonly Guid CommandSet = new Guid("f038e966-3a02-4eef-bfad-cd8fab3c4d6d");

        /// <summary>
        /// シングルトンのインスタンスを取得します。
        /// </summary>
        public static ConvertCaseCommand Instance { get; private set; }

        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        /// <param name="package">コマンドを提供するパッケージ</param>
        private ConvertCaseCommand(Package package) : base(package, CommandId, CommandSet)
        {
        }

        /// <summary>
        /// このコマンドのシングルトンのインスタンスを初期化します。
        /// </summary>
        /// <param name="package">コマンドを提供するパッケージ</param>
        public static void Initialize(Package package)
        {
            Instance = new ConvertCaseCommand(package);
        }

        /// <inheritdoc />
        protected override void Execute(object sender, EventArgs e)
        {
            var dte = ServiceProvider.GetService(typeof(DTE)) as DTE;
            var textDocument = dte.ActiveDocument.Object("TextDocument") as TextDocument;
            if (textDocument != null)
            {
                var selection = textDocument.Selection;
                if (selection.IsEmpty == false)
                {
                    var selectedText = selection.Text;
                    selection.ReplaceText(selectedText, StringCaseConverter.Convert(selectedText));
                }
                else
                {
                    var point = selection.ActivePoint;
                    var startPoint = CreateStartPoint(point);
                    var endPoint = CreateEndPoint(point, startPoint);

                    var targetText = startPoint.GetText(endPoint);
                    var word = targetText.TrimEnd(' ');
                    var convertedWord = StringCaseConverter.Convert(word);

                    if (word != convertedWord)
                    {
                        var left = point.AbsoluteCharOffset - startPoint.AbsoluteCharOffset;
                        selection.CharLeft(false, left);

                        var trimCount = targetText.Length - word.Length;
                        var right = endPoint.AbsoluteCharOffset - point.AbsoluteCharOffset - trimCount;
                        selection.CharRight(true, right);

                        selection.ReplaceText(word, convertedWord);
                    }
                }
            }
        }

        /// <summary>
        /// 文字列の終了位置を作成します。
        /// </summary>
        private static EditPoint CreateEndPoint(VirtualPoint point, EditPoint startPoint)
        {
            var result = point.CreateEditPoint();
            if (point.AtEndOfLine == false && result.GetText(1) != " ")
            {
                result = startPoint.CreateEditPoint();
                result.WordRight();
            }

            return result;
        }

        /// <summary>
        /// 文字列の開始位置を作成します。
        /// </summary>
        private static EditPoint CreateStartPoint(VirtualPoint point)
        {
            var result = point.CreateEditPoint();
            if (point.AtStartOfLine == false && GetLeftText(point) != " ")
            {
                result.WordLeft();

                var tempPoint = result.CreateEditPoint();
                tempPoint.WordRight();

                if (point.AbsoluteCharOffset == tempPoint.AbsoluteCharOffset)
                {
                    result = point.CreateEditPoint();
                }
            }

            return result;
        }

        /// <summary>
        /// 指定の位置の左の文字を取得します。
        /// </summary>
        private static string GetLeftText(VirtualPoint point)
        {
            var editPoint = point.CreateEditPoint();
            editPoint.CharLeft(1);

            return editPoint.GetText(1);
        }
    }
}
