namespace BondDesk.WinApp;

partial class BondDesk
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    private System.Windows.Forms.TextBox _filterNameTextBox;
    private System.Windows.Forms.TextBox _filterCouponTextBox;
    private System.Windows.Forms.TextBox _filterMaturityDateTextBox;
    private System.Windows.Forms.TextBox _filterEpicTextBox;
    private System.Windows.Forms.TextBox _filterDirtyPriceTextBox;
    private System.Windows.Forms.TextBox _filterCurrentYieldTextBox;
    private System.Windows.Forms.TextBox _filterAccruedInterestTextBox;
    private System.Windows.Forms.TextBox _filterConvexityTextBox;
    private System.Windows.Forms.TextBox _filterModifiedDurationTextBox;
    private System.Windows.Forms.TextBox _filterPresentValueOverDirtyTextBox;
    private System.Windows.Forms.TextBox _filterYieldToMaturityTextBox;
    private System.Windows.Forms.TextBox _filterPresentValueTextBox;
    private System.Windows.Forms.FlowLayoutPanel _bondsPanel;
    private System.Windows.Forms.Panel _filterSortPanel;

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
	private void InitializeComponent()
	{
		_filterNameTextBox = new TextBox();
		_filterCouponTextBox = new TextBox();
		_filterMaturityDateTextBox = new TextBox();
		_filterEpicTextBox = new TextBox();
		_filterDirtyPriceTextBox = new TextBox();
		_filterCurrentYieldTextBox = new TextBox();
		_filterAccruedInterestTextBox = new TextBox();
		_filterConvexityTextBox = new TextBox();
		_filterModifiedDurationTextBox = new TextBox();
		_filterPresentValueOverDirtyTextBox = new TextBox();
		_filterYieldToMaturityTextBox = new TextBox();
		 _filterPresentValueTextBox = new TextBox();
		_filterSortPanel = new Panel();
		_bondsPanel = new FlowLayoutPanel();
		_filterSortPanel.SuspendLayout();
		SuspendLayout();
		// 
		// _filterEpicTextBox
		// 
		_filterEpicTextBox.Location = new Point(0, 0);
		_filterEpicTextBox.Name = "_filterEpicTextBox";
		_filterEpicTextBox.Size = new Size(100, 23);
		_filterEpicTextBox.TabIndex = 0;
		// 
		// _filterNameTextBox
		// 
		_filterNameTextBox.Location = new Point(0, 0);
		_filterNameTextBox.Name = "_filterNameTextBox";
		_filterNameTextBox.Size = new Size(100, 23);
		_filterNameTextBox.TabIndex = 1;
		// 
		// _filterMaturityDateTextBox
		// 
		_filterMaturityDateTextBox.Location = new Point(0, 0);
		_filterMaturityDateTextBox.Name = "_filterMaturityDateTextBox";
		_filterMaturityDateTextBox.Size = new Size(100, 23);
		_filterMaturityDateTextBox.TabIndex = 2;
		// 
		// _filterAccruedInterestTextBox
		// 
		_filterAccruedInterestTextBox.Location = new Point(0, 0);
		_filterAccruedInterestTextBox.Name = "_filterAccruedInterestTextBox";
		_filterAccruedInterestTextBox.Size = new Size(100, 23);
		_filterAccruedInterestTextBox.TabIndex = 3;
		// 
		// _filterCouponTextBox
		// 
		_filterCouponTextBox.Location = new Point(0, 0);
		_filterCouponTextBox.Name = "_filterCouponTextBox";
		_filterCouponTextBox.Size = new Size(100, 23);
		_filterCouponTextBox.TabIndex = 4;
		// 
		// _filterYieldToMaturityTextBox
		// 
		_filterYieldToMaturityTextBox.Location = new Point(0, 0);
		_filterYieldToMaturityTextBox.Name = "_filterYieldToMaturityTextBox";
		_filterYieldToMaturityTextBox.Size = new Size(100, 23);
		_filterYieldToMaturityTextBox.TabIndex = 5;
		// 
		// _filterCurrentYieldTextBox
		// 
		_filterCurrentYieldTextBox.Location = new Point(0, 0);
		_filterCurrentYieldTextBox.Name = "_filterCurrentYieldTextBox";
		_filterCurrentYieldTextBox.Size = new Size(100, 23);
		_filterCurrentYieldTextBox.TabIndex = 6;
		// 
		// _filterDirtyPriceTextBox
		// 
		_filterDirtyPriceTextBox.Location = new Point(0, 0);
		_filterDirtyPriceTextBox.Name = "_filterDirtyPriceTextBox";
		_filterDirtyPriceTextBox.Size = new Size(100, 23);
		_filterDirtyPriceTextBox.TabIndex = 7;
		// 
		// _filterPresentValueOverDirtyTextBox
		// 
		_filterPresentValueOverDirtyTextBox.Location = new Point(0, 0);
		_filterPresentValueOverDirtyTextBox.Name = "_filterPresentValueOverDirtyTextBox";
		_filterPresentValueOverDirtyTextBox.Size = new Size(100, 23);
		_filterPresentValueOverDirtyTextBox.TabIndex = 8;
		// 
		// _filterModifiedDurationTextBox
		// 
		_filterModifiedDurationTextBox.Location = new Point(0, 0);
		_filterModifiedDurationTextBox.Name = "_filterModifiedDurationTextBox";
		_filterModifiedDurationTextBox.Size = new Size(100, 23);
		_filterModifiedDurationTextBox.TabIndex = 9;
		// 
		// _filterConvexityTextBox
		// 
		_filterConvexityTextBox.Location = new Point(0, 0);
		_filterConvexityTextBox.Name = "_filterConvexityTextBox";
		_filterConvexityTextBox.Size = new Size(100, 23);
		_filterConvexityTextBox.TabIndex = 10;
		// 
		// _filterPresentValueTextBox
		// 
		_filterPresentValueTextBox.Location = new Point(0, 0);
		_filterPresentValueTextBox.Name = "_filterPresentValueTextBox";
		_filterPresentValueTextBox.Size = new Size(100, 23);
		_filterPresentValueTextBox.TabIndex = 11;
		// 
		// _filterSortPanel
		// 
		_filterSortPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
		_filterSortPanel.BackColor = Color.FromArgb(30, 34, 36);
		_filterSortPanel.Controls.Add(_filterNameTextBox);
		_filterSortPanel.Controls.Add(_filterCouponTextBox);
		_filterSortPanel.Controls.Add(_filterMaturityDateTextBox);
		_filterSortPanel.Controls.Add(_filterEpicTextBox);
		_filterSortPanel.Controls.Add(_filterDirtyPriceTextBox);
		_filterSortPanel.Controls.Add(_filterCurrentYieldTextBox);
		_filterSortPanel.Controls.Add(_filterAccruedInterestTextBox);
		_filterSortPanel.Controls.Add(_filterConvexityTextBox);
		_filterSortPanel.Controls.Add(_filterModifiedDurationTextBox);
		_filterSortPanel.Controls.Add(_filterPresentValueOverDirtyTextBox);
		_filterSortPanel.Controls.Add(_filterYieldToMaturityTextBox);
		_filterSortPanel.Controls.Add(_filterPresentValueTextBox);
		_filterSortPanel.Location = new Point(0, 0);
		_filterSortPanel.Name = "_filterSortPanel";
		_filterSortPanel.Size = new Size(1200, 50);
		_filterSortPanel.TabIndex = 1;
		// 
		// _bondsPanel
		// 
		_bondsPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
		_bondsPanel.AutoScroll = true;
		_bondsPanel.BackColor = Color.FromArgb(20, 24, 26);
		_bondsPanel.Location = new Point(0, 70);
		_bondsPanel.Name = "_bondsPanel";
		_bondsPanel.Size = new Size(1200, 500);
		_bondsPanel.TabIndex = 0;
		// 
		// BondDesk
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		BackColor = Color.FromArgb(20, 24, 26);
		ClientSize = new Size(1200, 600);
		Controls.Add(_bondsPanel);
		Controls.Add(_filterSortPanel);
		Font = new Font("Consolas", 10F);
		ForeColor = Color.White;
		Name = "BondDesk";
		ShowIcon = false;
		Text = "Bond Desk";
		_filterSortPanel.ResumeLayout(false);
		_filterSortPanel.PerformLayout();
		ResumeLayout(false);
	}

	#endregion
}
