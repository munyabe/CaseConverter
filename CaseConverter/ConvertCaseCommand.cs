using System;
using System.Collections.Generic;
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
        /// 文字列の変換パターンです。
        /// </summary>
        private readonly IList<StringCasePattern> _convertPatterns = new List<StringCasePattern> { StringCasePattern.CamelCase, StringCasePattern.PascalCase, StringCasePattern.SnakeCase };

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
                    selection.ReplaceText(selectedText, StringCaseConverter.Convert(selectedText, _convertPatterns));
                }
                else
                {
                    var point = selection.ActivePoint;
                    var startPoint = CreateStartPoint(point);
                    var endPoint = CreateEndPoint(point, startPoint);

                    var targetText = startPoint.GetText(endPoint);
                    var word = targetText.TrimEnd(' ');
                    var convertedWord = StringCaseConverter.Convert(word, _convertPatterns);

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
            if (point.AtEndOfLine || result.GetText(1) == " ")
            {
                return result;
            }

            result = startPoint.CreateEditPoint();
            result.WordRight();
            return result;
        }

        /// <summary>
        /// 文字列の開始位置を作成します。
        /// </summary>
        private static EditPoint CreateStartPoint(VirtualPoint point)
        {
            var result = point.CreateEditPoint();
            if (point.AtStartOfLine || GetLeftText(point, 1) == " ")
            {
                return result;
            }

            result.WordLeft();

            var tempPoint = result.CreateEditPoint();
            tempPoint.WordRight();

            return point.AbsoluteCharOffset == tempPoint.AbsoluteCharOffset ?
                point.CreateEditPoint() : result;
        }

        /// <summary>
        /// 指定の位置の左の文字を取得します。
        /// </summary>
        private static string GetLeftText(VirtualPoint point, int count)
        {
            var editPoint = point.CreateEditPoint();
            editPoint.CharLeft(count);

            return editPoint.GetText(1);
        }
    }
}
