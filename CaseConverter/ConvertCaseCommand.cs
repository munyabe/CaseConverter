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
                var selectedText = selection.Text;
                selection.ReplaceText(selectedText, StringCaseConverter.Convert(selectedText));
            }
        }
    }
}
