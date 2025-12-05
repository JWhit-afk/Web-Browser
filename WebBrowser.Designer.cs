namespace Web_Browser_CW1
{
    partial class WebBrowser
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            components = new System.ComponentModel.Container();
            container = new Panel();
            searchPannel = new Panel();
            nextPage = new Button();
            favourite = new Button();
            previousPage = new Button();
            home = new Button();
            searchButton = new Button();
            refresh = new Button();
            urlBar = new TextBox();
            htmlPanel = new Panel();
            progressBar = new ProgressBar();
            htmlDisplay = new TextBox();
            menuStrip = new MenuStrip();
            favouritesToolStripMenuItem = new ToolStripMenuItem();
            historyToolStripMenuItem = new ToolStripMenuItem();
            downloadToolStripMenuItem = new ToolStripMenuItem();
            DownloadFileName = new ToolStripTextBox();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            changeHomepageToolStripMenuItem = new ToolStripMenuItem();
            enterNewHomepageToolStripMenuItem = new ToolStripMenuItem();
            newHomepageText = new ToolStripTextBox();
            attributesToolStripMenuItem = new ToolStripMenuItem();
            statusStrip = new StatusStrip();
            HtmlResponseCodeOutput = new ToolStripStatusLabel();
            toolTips = new ToolTip(components);
            container.SuspendLayout();
            searchPannel.SuspendLayout();
            htmlPanel.SuspendLayout();
            menuStrip.SuspendLayout();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // container
            // 
            container.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            container.Controls.Add(searchPannel);
            container.Controls.Add(htmlPanel);
            container.Location = new Point(9, 36);
            container.Margin = new Padding(4);
            container.Name = "container";
            container.Size = new Size(988, 490);
            container.TabIndex = 10;
            // 
            // searchPannel
            // 
            searchPannel.Controls.Add(nextPage);
            searchPannel.Controls.Add(favourite);
            searchPannel.Controls.Add(previousPage);
            searchPannel.Controls.Add(home);
            searchPannel.Controls.Add(searchButton);
            searchPannel.Controls.Add(refresh);
            searchPannel.Controls.Add(urlBar);
            searchPannel.Location = new Point(9, 1);
            searchPannel.Margin = new Padding(4);
            searchPannel.Name = "searchPannel";
            searchPannel.Size = new Size(979, 40);
            searchPannel.TabIndex = 9;
            // 
            // nextPage
            // 
            nextPage.BackColor = SystemColors.ControlLightLight;
            nextPage.BackgroundImage = Properties.Resources.arrow_right;
            nextPage.BackgroundImageLayout = ImageLayout.Stretch;
            nextPage.Enabled = false;
            nextPage.Location = new Point(91, 1);
            nextPage.Margin = new Padding(4);
            nextPage.Name = "nextPage";
            nextPage.Size = new Size(34, 34);
            nextPage.TabIndex = 10;
            toolTips.SetToolTip(nextPage, "Next Page (Right Arrow)");
            nextPage.UseVisualStyleBackColor = false;
            nextPage.Visible = false;
            nextPage.Click += NextClick;
            // 
            // favourite
            // 
            favourite.BackgroundImage = Properties.Resources.star_empty;
            favourite.BackgroundImageLayout = ImageLayout.Stretch;
            favourite.Location = new Point(829, 1);
            favourite.Margin = new Padding(4);
            favourite.Name = "favourite";
            favourite.Size = new Size(34, 34);
            favourite.TabIndex = 2;
            toolTips.SetToolTip(favourite, "Favorite/Un-Favourite (Ctrl+B)");
            favourite.UseVisualStyleBackColor = true;
            // 
            // previousPage
            // 
            previousPage.BackColor = SystemColors.ControlLightLight;
            previousPage.BackgroundImage = Properties.Resources.arrow_left;
            previousPage.BackgroundImageLayout = ImageLayout.Stretch;
            previousPage.Enabled = false;
            previousPage.ForeColor = SystemColors.ControlText;
            previousPage.Location = new Point(50, 1);
            previousPage.Margin = new Padding(4);
            previousPage.Name = "previousPage";
            previousPage.Size = new Size(34, 34);
            previousPage.TabIndex = 9;
            toolTips.SetToolTip(previousPage, "Previous Page (Right Arrow)");
            previousPage.UseVisualStyleBackColor = false;
            previousPage.Visible = false;
            previousPage.Click += PrevClick;
            // 
            // home
            // 
            home.BackgroundImage = Properties.Resources.home;
            home.BackgroundImageLayout = ImageLayout.Stretch;
            home.Location = new Point(9, 1);
            home.Margin = new Padding(4);
            home.Name = "home";
            home.Size = new Size(34, 34);
            home.TabIndex = 8;
            toolTips.SetToolTip(home, "Home (Ctrl+H)");
            home.UseVisualStyleBackColor = true;
            home.Click += HomeClick;
            // 
            // searchButton
            // 
            searchButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            searchButton.Location = new Point(874, 1);
            searchButton.Margin = new Padding(4);
            searchButton.Name = "searchButton";
            searchButton.Size = new Size(98, 34);
            searchButton.TabIndex = 7;
            searchButton.Text = "Search";
            toolTips.SetToolTip(searchButton, "Search (Enter)");
            searchButton.UseVisualStyleBackColor = true;
            searchButton.Click += SearchClick;
            // 
            // refresh
            // 
            refresh.BackgroundImage = Properties.Resources.arrow_spin;
            refresh.BackgroundImageLayout = ImageLayout.Stretch;
            refresh.Location = new Point(788, 1);
            refresh.Margin = new Padding(4);
            refresh.Name = "refresh";
            refresh.Size = new Size(34, 34);
            refresh.TabIndex = 0;
            toolTips.SetToolTip(refresh, "Refresh (F5)");
            refresh.UseVisualStyleBackColor = true;
            // 
            // urlBar
            // 
            urlBar.Anchor = AnchorStyles.Top;
            urlBar.Location = new Point(134, 3);
            urlBar.Margin = new Padding(4);
            urlBar.Name = "urlBar";
            urlBar.Size = new Size(650, 31);
            urlBar.TabIndex = 3;
            toolTips.SetToolTip(urlBar, "Searchbar (Press Enter to Search)");
            // 
            // htmlPanel
            // 
            htmlPanel.Controls.Add(progressBar);
            htmlPanel.Controls.Add(htmlDisplay);
            htmlPanel.Location = new Point(14, 44);
            htmlPanel.Margin = new Padding(4);
            htmlPanel.Name = "htmlPanel";
            htmlPanel.Size = new Size(962, 442);
            htmlPanel.TabIndex = 8;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(82, 200);
            progressBar.Margin = new Padding(4);
            progressBar.MarqueeAnimationSpeed = 10;
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(775, 31);
            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.TabIndex = 9;
            // 
            // htmlDisplay
            // 
            htmlDisplay.Location = new Point(4, 4);
            htmlDisplay.Margin = new Padding(4);
            htmlDisplay.Multiline = true;
            htmlDisplay.Name = "htmlDisplay";
            htmlDisplay.ScrollBars = ScrollBars.Vertical;
            htmlDisplay.Size = new Size(962, 442);
            htmlDisplay.TabIndex = 6;
            // 
            // menuStrip
            // 
            menuStrip.ImageScalingSize = new Size(20, 20);
            menuStrip.Items.AddRange(new ToolStripItem[] { favouritesToolStripMenuItem, historyToolStripMenuItem, downloadToolStripMenuItem, settingsToolStripMenuItem, attributesToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Padding = new Padding(8, 2, 0, 2);
            menuStrip.Size = new Size(1000, 33);
            menuStrip.TabIndex = 1;
            menuStrip.Text = "menuStrip1";
            // 
            // favouritesToolStripMenuItem
            // 
            favouritesToolStripMenuItem.Name = "favouritesToolStripMenuItem";
            favouritesToolStripMenuItem.Size = new Size(108, 29);
            favouritesToolStripMenuItem.Text = "Favourites";
            // 
            // historyToolStripMenuItem
            // 
            historyToolStripMenuItem.Name = "historyToolStripMenuItem";
            historyToolStripMenuItem.Size = new Size(85, 29);
            historyToolStripMenuItem.Text = "History";
            // 
            // downloadToolStripMenuItem
            // 
            downloadToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { DownloadFileName });
            downloadToolStripMenuItem.Name = "downloadToolStripMenuItem";
            downloadToolStripMenuItem.Size = new Size(110, 29);
            downloadToolStripMenuItem.Text = "Download";
            // 
            // DownloadFileName
            // 
            DownloadFileName.Name = "DownloadFileName";
            DownloadFileName.Size = new Size(121, 31);
            DownloadFileName.Text = "bulk.txt";
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { changeHomepageToolStripMenuItem });
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(92, 29);
            settingsToolStripMenuItem.Text = "Settings";
            // 
            // changeHomepageToolStripMenuItem
            // 
            changeHomepageToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { enterNewHomepageToolStripMenuItem, newHomepageText });
            changeHomepageToolStripMenuItem.Name = "changeHomepageToolStripMenuItem";
            changeHomepageToolStripMenuItem.Size = new Size(270, 34);
            changeHomepageToolStripMenuItem.Text = "Change Homepage";
            // 
            // enterNewHomepageToolStripMenuItem
            // 
            enterNewHomepageToolStripMenuItem.Name = "enterNewHomepageToolStripMenuItem";
            enterNewHomepageToolStripMenuItem.Size = new Size(334, 34);
            enterNewHomepageToolStripMenuItem.Text = "Enter New Homepage Here:";
            // 
            // newHomepageText
            // 
            newHomepageText.Name = "newHomepageText";
            newHomepageText.Size = new Size(100, 31);
            newHomepageText.KeyDown += NewHomePageEnter;
            // 
            // attributesToolStripMenuItem
            // 
            attributesToolStripMenuItem.Name = "attributesToolStripMenuItem";
            attributesToolStripMenuItem.Size = new Size(107, 29);
            attributesToolStripMenuItem.Text = "Resources";
            // 
            // statusStrip
            // 
            statusStrip.ImageScalingSize = new Size(20, 20);
            statusStrip.Items.AddRange(new ToolStripItem[] { HtmlResponseCodeOutput });
            statusStrip.Location = new Point(0, 530);
            statusStrip.Name = "statusStrip";
            statusStrip.Padding = new Padding(1, 0, 18, 0);
            statusStrip.Size = new Size(1000, 32);
            statusStrip.TabIndex = 11;
            statusStrip.Text = "statusStrip1";
            // 
            // HtmlResponseCodeOutput
            // 
            HtmlResponseCodeOutput.Name = "HtmlResponseCodeOutput";
            HtmlResponseCodeOutput.Size = new Size(139, 25);
            HtmlResponseCodeOutput.Text = "Response Code:";
            // 
            // WebBrowser
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 562);
            Controls.Add(statusStrip);
            Controls.Add(container);
            Controls.Add(menuStrip);
            KeyPreview = true;
            MainMenuStrip = menuStrip;
            Margin = new Padding(4);
            Name = "WebBrowser";
            Text = "Web Browser";
            container.ResumeLayout(false);
            searchPannel.ResumeLayout(false);
            searchPannel.PerformLayout();
            htmlPanel.ResumeLayout(false);
            htmlPanel.PerformLayout();
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip;
        private ToolStripMenuItem favouritesToolStripMenuItem;
        private ToolStripMenuItem historyToolStripMenuItem;
        private ToolStripMenuItem downloadToolStripMenuItem;
        private ToolStripMenuItem attributesToolStripMenuItem;
        private TextBox htmlDisplay;
        private ToolTip toolTips;
        private Panel htmlPanel;
        private ProgressBar progressBar;
        private Panel container;
        private Panel searchPannel;
        private Button nextPage;
        private Button favourite;
        private Button previousPage;
        private Button home;
        private Button searchButton;
        private Button refresh;
        private TextBox urlBar;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel HtmlResponseCodeOutput;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem changeHomepageToolStripMenuItem;
        private ToolStripMenuItem enterNewHomepageToolStripMenuItem;
        private ToolStripTextBox newHomepageText;
        private ToolStripTextBox DownloadFileName;
    }
}
