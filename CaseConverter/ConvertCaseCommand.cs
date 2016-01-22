using System;
using System.ComponentModel.Design;
using System.Globalization;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace CaseConverter
{
    /// <summary>
    /// 文字列をキャメルケース⇔スネークケースに変換するコマンドです。
    /// </summary>
    internal sealed class ConvertCaseCommand
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
        /// コマンドを提供するパッケージです。
        /// </summary>
        private readonly Package package;

        /// <summary>
        /// インスタンスを初期化します。
        /// コマンドは .vsct ファイルに定義されている必要があります。
        /// </summary>
        /// <param name="package">コマンドを提供するパッケージ</param>
        private ConvertCaseCommand(Package package)
        {
            if (package == null)
            {
                throw new ArgumentNullException("package");
            }

            this.package = package;

            OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                var menuCommandID = new CommandID(CommandSet, CommandId);
                var menuItem = new MenuCommand(this.MenuItemCallback, menuCommandID);
                commandService.AddCommand(menuItem);
            }
        }

        /// <summary>
        /// シングルトンのインスタンスです。
        /// </summary>
        public static ConvertCaseCommand Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// サービスプロバイダーを取得します。
        /// </summary>
        private IServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// このコマンドのシングルトンのインスタンスを初期化します。
        /// </summary>
        /// <param name="package">コマンドを提供するパッケージ</param>
        public static void Initialize(Package package)
        {
            Instance = new ConvertCaseCommand(package);
        }

        /// <summary>
        /// コマンドを実行します。
        /// </summary>
        /// <param name="sender">イベントの発行者</param>
        /// <param name="e">イベント引数</param>
        private void MenuItemCallback(object sender, EventArgs e)
        {
            string message = string.Format(CultureInfo.CurrentCulture, "Inside {0}.MenuItemCallback()", this.GetType().FullName);
            string title = "ConvertCaseCommand";

            // Show a message box to prove we were here
            VsShellUtilities.ShowMessageBox(
                this.ServiceProvider,
                message,
                title,
                OLEMSGICON.OLEMSGICON_INFO,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
        }
    }
}
