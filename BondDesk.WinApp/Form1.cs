using BondDesk.BondProvider;
using BondDesk.DateTimeProvider;
using BondDesk.Domain.Interfaces.Services;
using BondDesk.GiltsInIssueRepo;
using BondDesk.QuoteProvider;
using System.Globalization;

namespace BondDesk.WinApp;

public partial class GiltsViewer : Form
{
	private readonly IGiltsService _giltsService;
	private List<BondViewModel> _bonds = new();
	private bool _sortAscending = true;

	public GiltsViewer()
	{
		_giltsService = new GiltsService(new CachedRepo(), new Gilts(), new SimpleDateTimeProvider());
		InitializeComponent();

		// Wire up all filter TextBoxes
		_filterNameTextBox.TextChanged += (s, e) => UpdateBondPanels();
		_filterCouponTextBox.TextChanged += (s, e) => UpdateBondPanels();
		_filterMaturityDateTextBox.TextChanged += (s, e) => UpdateBondPanels();
		_filterEpicTextBox.TextChanged += (s, e) => UpdateBondPanels();
		_filterDirtyPriceTextBox.TextChanged += (s, e) => UpdateBondPanels();
		_filterCurrentYieldTextBox.TextChanged += (s, e) => UpdateBondPanels();
		_filterAccruedInterestTextBox.TextChanged += (s, e) => UpdateBondPanels();
		_filterConvexityTextBox.TextChanged += (s, e) => UpdateBondPanels();
		_filterModifiedDurationTextBox.TextChanged += (s, e) => UpdateBondPanels();
		_filterPresentValueOverDirtyTextBox.TextChanged += (s, e) => UpdateBondPanels();
		_filterYieldToMaturityTextBox.TextChanged += (s, e) => UpdateBondPanels();
		_filterOfferQtyTextBox.TextChanged += (s, e) => UpdateBondPanels();

		_sortComboBox.SelectedIndexChanged += (s, e) => UpdateBondPanels();
		_sortOrderButton.Click += (s, e) => { _sortAscending = !_sortAscending; _sortOrderButton.Text = _sortAscending ? "Asc" : "Desc"; UpdateBondPanels(); };
		Load += Form1_Load;
		Resize += (s, e) => ResizeFilterControls();
	}

	private async void Form1_Load(object? sender, EventArgs e)
	{
		_bonds.Clear();
		await foreach (var bond in _giltsService.GetGiltsAsync())
		{
			_bonds.Add(new BondViewModel
			{
				Name = bond.Name,
				Coupon = bond.Coupon,
				MaturityDate = bond.MaturityDate,
				Epic = bond.Epic,
				DirtyPrice = bond.DirtyPrice,
				CurrentYield = bond.CurrentYield,
				AccruedInterest = bond.AccruedInterest,
				Convexity = bond.Convexity,
				ModifiedDuration = bond.ModifiedDuration,
				PresentValueOverDirty = bond.PresentValueOverDirty,
				YieldToMaturity = bond.YieldToMaturity,
				OfferQty = bond.OfferQty
			});
		}
		UpdateBondPanels();
	}

	private void ResizeFilterControls()
	{
		// Dynamically set widths so all filter boxes and sort controls fill the width
		int totalFilters = 12;
		int spacing = 8;
		int left = 12;
		int sortControlsWidth = 290; // _sortComboBox (200) + _sortOrderButton (80) + spacing (10)
		int availableWidth = ClientSize.Width - (left * 2) - (spacing * (totalFilters - 1)) - sortControlsWidth;
		int boxWidth = availableWidth / totalFilters;
		TextBox[] filters = {
			_filterNameTextBox, _filterCouponTextBox, _filterMaturityDateTextBox, _filterEpicTextBox, _filterDirtyPriceTextBox,
			_filterCurrentYieldTextBox, _filterAccruedInterestTextBox, _filterConvexityTextBox, _filterModifiedDurationTextBox,
			_filterPresentValueOverDirtyTextBox, _filterYieldToMaturityTextBox, _filterOfferQtyTextBox
		};
		for (int i = 0; i < filters.Length; i++)
		{
			filters[i].Left = left + i * (boxWidth + spacing);
			filters[i].Width = boxWidth;
		}
		// Place sort controls at the right
		int sortLeft = left + totalFilters * (boxWidth + spacing);
		_sortComboBox.Left = sortLeft;
		_sortOrderButton.Left = sortLeft + _sortComboBox.Width + 10;
	}

	private void UpdateBondPanels()
	{
		var darkBg = System.Drawing.Color.FromArgb(20, 24, 26);
		var panelBg = System.Drawing.Color.FromArgb(30, 34, 36);
		var textGreen = System.Drawing.Color.FromArgb(0, 255, 0);
		var textOrange = System.Drawing.Color.FromArgb(255, 140, 0);
		var textYellow = System.Drawing.Color.FromArgb(255, 255, 0);
		var textWhite = System.Drawing.Color.White;
		var textGray = System.Drawing.Color.FromArgb(180, 180, 180);
		var font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular);
		var headerFont = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold);

		_bondsPanel.SuspendLayout();
		_bondsPanel.Controls.Clear();
		var filtered = _bonds.Where(b =>
			(string.IsNullOrWhiteSpace(_filterNameTextBox.Text) || (b.Name?.IndexOf(_filterNameTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0)) &&
			(string.IsNullOrWhiteSpace(_filterCouponTextBox.Text) || FilterDecimal(b.Coupon, _filterCouponTextBox.Text)) &&
			(string.IsNullOrWhiteSpace(_filterMaturityDateTextBox.Text) || b.MaturityDate.ToString("yyyy-MM-dd").Contains(_filterMaturityDateTextBox.Text, StringComparison.OrdinalIgnoreCase)) &&
			(string.IsNullOrWhiteSpace(_filterEpicTextBox.Text) || (b.Epic?.IndexOf(_filterEpicTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0)) &&
			(string.IsNullOrWhiteSpace(_filterDirtyPriceTextBox.Text) || FilterDecimal(b.DirtyPrice, _filterDirtyPriceTextBox.Text)) &&
			(string.IsNullOrWhiteSpace(_filterCurrentYieldTextBox.Text) || FilterDecimal(b.CurrentYield, _filterCurrentYieldTextBox.Text)) &&
			(string.IsNullOrWhiteSpace(_filterAccruedInterestTextBox.Text) || FilterDecimal(b.AccruedInterest, _filterAccruedInterestTextBox.Text)) &&
			(string.IsNullOrWhiteSpace(_filterConvexityTextBox.Text) || FilterDecimal(b.Convexity, _filterConvexityTextBox.Text)) &&
			(string.IsNullOrWhiteSpace(_filterModifiedDurationTextBox.Text) || FilterDecimal(b.ModifiedDuration, _filterModifiedDurationTextBox.Text)) &&
			(string.IsNullOrWhiteSpace(_filterPresentValueOverDirtyTextBox.Text) || FilterDecimal(b.PresentValueOverDirty, _filterPresentValueOverDirtyTextBox.Text)) &&
			(string.IsNullOrWhiteSpace(_filterYieldToMaturityTextBox.Text) || FilterDecimal(b.YieldToMaturity, _filterYieldToMaturityTextBox.Text)) &&
			(string.IsNullOrWhiteSpace(_filterOfferQtyTextBox.Text) || (b.OfferQty != null && FilterDecimal(b.OfferQty.Value, _filterOfferQtyTextBox.Text)))
		).ToList();

		var sortProp = _sortComboBox.SelectedItem?.ToString();
		if (!string.IsNullOrEmpty(sortProp))
		{
			filtered = _sortAscending
				? filtered.OrderBy(b => b.GetPropertyValue(sortProp)).ToList()
				: filtered.OrderByDescending(b => b.GetPropertyValue(sortProp)).ToList();
		}

		int panelWidth = _bondsPanel.ClientSize.Width - 10;
		int labelCount = 12;
		int labelSpacing = 8;
		int labelWidth = (panelWidth - (labelSpacing * (labelCount - 1))) / labelCount;

		// Add header row
		var headerPanel = new Panel { Height = 40, Dock = DockStyle.Top, Width = panelWidth, Margin = new Padding(3), BackColor = darkBg };
		int headerLeft = 0;
		headerPanel.Controls.Add(new Label { Text = "Name", Left = headerLeft, Top = 8, Width = labelWidth, AutoSize = false, ForeColor = textGreen, Font = headerFont, BackColor = darkBg, TextAlign = System.Drawing.ContentAlignment.MiddleCenter }); headerLeft += labelWidth + labelSpacing;
		headerPanel.Controls.Add(new Label { Text = "Epic", Left = headerLeft, Top = 8, Width = labelWidth, AutoSize = false, ForeColor = textOrange, Font = headerFont, BackColor = darkBg, TextAlign = System.Drawing.ContentAlignment.MiddleCenter }); headerLeft += labelWidth + labelSpacing;
		headerPanel.Controls.Add(new Label { Text = "Maturity", Left = headerLeft, Top = 8, Width = labelWidth, AutoSize = false, ForeColor = textYellow, Font = headerFont, BackColor = darkBg, TextAlign = System.Drawing.ContentAlignment.MiddleCenter }); headerLeft += labelWidth + labelSpacing;
		headerPanel.Controls.Add(new Label { Text = "Coupon", Left = headerLeft, Top = 8, Width = labelWidth, AutoSize = false, ForeColor = textGreen, Font = headerFont, BackColor = darkBg, TextAlign = System.Drawing.ContentAlignment.MiddleCenter }); headerLeft += labelWidth + labelSpacing;
		headerPanel.Controls.Add(new Label { Text = "Dirty", Left = headerLeft, Top = 8, Width = labelWidth, AutoSize = false, ForeColor = textWhite, Font = headerFont, BackColor = darkBg, TextAlign = System.Drawing.ContentAlignment.MiddleCenter }); headerLeft += labelWidth + labelSpacing;
		headerPanel.Controls.Add(new Label { Text = "Yield", Left = headerLeft, Top = 8, Width = labelWidth, AutoSize = false, ForeColor = textGreen, Font = headerFont, BackColor = darkBg, TextAlign = System.Drawing.ContentAlignment.MiddleCenter }); headerLeft += labelWidth + labelSpacing;
		headerPanel.Controls.Add(new Label { Text = "Accrued", Left = headerLeft, Top = 8, Width = labelWidth, AutoSize = false, ForeColor = textGray, Font = headerFont, BackColor = darkBg, TextAlign = System.Drawing.ContentAlignment.MiddleCenter }); headerLeft += labelWidth + labelSpacing;
		headerPanel.Controls.Add(new Label { Text = "Convexity", Left = headerLeft, Top = 8, Width = labelWidth, AutoSize = false, ForeColor = textOrange, Font = headerFont, BackColor = darkBg, TextAlign = System.Drawing.ContentAlignment.MiddleCenter }); headerLeft += labelWidth + labelSpacing;
		headerPanel.Controls.Add(new Label { Text = "ModDur", Left = headerLeft, Top = 8, Width = labelWidth, AutoSize = false, ForeColor = textYellow, Font = headerFont, BackColor = darkBg, TextAlign = System.Drawing.ContentAlignment.MiddleCenter }); headerLeft += labelWidth + labelSpacing;
		headerPanel.Controls.Add(new Label { Text = "PV/Dirty", Left = headerLeft, Top = 8, Width = labelWidth, AutoSize = false, ForeColor = textWhite, Font = headerFont, BackColor = darkBg, TextAlign = System.Drawing.ContentAlignment.MiddleCenter }); headerLeft += labelWidth + labelSpacing;
		headerPanel.Controls.Add(new Label { Text = "YTM", Left = headerLeft, Top = 8, Width = labelWidth, AutoSize = false, ForeColor = textGreen, Font = headerFont, BackColor = darkBg, TextAlign = System.Drawing.ContentAlignment.MiddleCenter }); headerLeft += labelWidth + labelSpacing;
		headerPanel.Controls.Add(new Label { Text = "OfferQty", Left = headerLeft, Top = 8, Width = labelWidth, AutoSize = false, ForeColor = textOrange, Font = headerFont, BackColor = darkBg, TextAlign = System.Drawing.ContentAlignment.MiddleCenter });
		_bondsPanel.Controls.Add(headerPanel);

		foreach (var bond in filtered)
		{
			var panel = new Panel { Height = 60, Dock = DockStyle.Top, Width = panelWidth, Margin = new Padding(3), BackColor = panelBg };
			int left = 0;
			// First row: values
			panel.Controls.Add(new Label { Text = bond.Name, Left = left, Top = 8, Width = labelWidth, AutoSize = false, ForeColor = textGreen, Font = font, BackColor = panelBg, TextAlign = System.Drawing.ContentAlignment.MiddleCenter }); left += labelWidth + labelSpacing;
			panel.Controls.Add(new Label { Text = bond.Epic, Left = left, Top = 8, Width = labelWidth, AutoSize = false, ForeColor = textOrange, Font = font, BackColor = panelBg, TextAlign = System.Drawing.ContentAlignment.MiddleCenter }); left += labelWidth + labelSpacing;
			panel.Controls.Add(new Label { Text = bond.MaturityDate.ToString("yyyy-MM-dd"), Left = left, Top = 8, Width = labelWidth, AutoSize = false, ForeColor = textYellow, Font = font, BackColor = panelBg, TextAlign = System.Drawing.ContentAlignment.MiddleCenter }); left += labelWidth + labelSpacing;
			panel.Controls.Add(new Label { Text = bond.Coupon.ToString("F4"), Left = left, Top = 8, Width = labelWidth, AutoSize = false, ForeColor = textGreen, Font = font, BackColor = panelBg, TextAlign = System.Drawing.ContentAlignment.MiddleCenter }); left += labelWidth + labelSpacing;
			panel.Controls.Add(new Label { Text = bond.DirtyPrice.ToString("F4"), Left = left, Top = 8, Width = labelWidth, AutoSize = false, ForeColor = textWhite, Font = font, BackColor = panelBg, TextAlign = System.Drawing.ContentAlignment.MiddleCenter }); left += labelWidth + labelSpacing;
			panel.Controls.Add(new Label { Text = bond.CurrentYield.ToString("F4"), Left = left, Top = 8, Width = labelWidth, AutoSize = false, ForeColor = textGreen, Font = font, BackColor = panelBg, TextAlign = System.Drawing.ContentAlignment.MiddleCenter }); left += labelWidth + labelSpacing;
			panel.Controls.Add(new Label { Text = bond.AccruedInterest.ToString("F4"), Left = left, Top = 8, Width = labelWidth, AutoSize = false, ForeColor = textGray, Font = font, BackColor = panelBg, TextAlign = System.Drawing.ContentAlignment.MiddleCenter }); left += labelWidth + labelSpacing;
			panel.Controls.Add(new Label { Text = bond.Convexity.ToString("F4"), Left = left, Top = 8, Width = labelWidth, AutoSize = false, ForeColor = textOrange, Font = font, BackColor = panelBg, TextAlign = System.Drawing.ContentAlignment.MiddleCenter }); left += labelWidth + labelSpacing;
			panel.Controls.Add(new Label { Text = bond.ModifiedDuration.ToString("F4"), Left = left, Top = 8, Width = labelWidth, AutoSize = false, ForeColor = textYellow, Font = font, BackColor = panelBg, TextAlign = System.Drawing.ContentAlignment.MiddleCenter }); left += labelWidth + labelSpacing;
			panel.Controls.Add(new Label { Text = bond.PresentValueOverDirty.ToString("F4"), Left = left, Top = 8, Width = labelWidth, AutoSize = false, ForeColor = textWhite, Font = font, BackColor = panelBg, TextAlign = System.Drawing.ContentAlignment.MiddleCenter }); left += labelWidth + labelSpacing;
			panel.Controls.Add(new Label { Text = bond.YieldToMaturity.ToString("F4"), Left = left, Top = 8, Width = labelWidth, AutoSize = false, ForeColor = textGreen, Font = font, BackColor = panelBg, TextAlign = System.Drawing.ContentAlignment.MiddleCenter }); left += labelWidth + labelSpacing;
			panel.Controls.Add(new Label { Text = bond.OfferQty?.ToString() ?? string.Empty, Left = left, Top = 8, Width = labelWidth, AutoSize = false, ForeColor = textOrange, Font = font, BackColor = panelBg, TextAlign = System.Drawing.ContentAlignment.MiddleCenter });
			_bondsPanel.Controls.Add(panel);
		}
		_bondsPanel.ResumeLayout();
	}

	private bool TryParseDecimalRange(string input, out decimal? min, out decimal? max)
	{
		min = null;
		max = null;
		if (string.IsNullOrWhiteSpace(input)) return false;
		var parts = input.Split('-');
		if (parts.Length == 1)
		{
			if (decimal.TryParse(parts[0], NumberStyles.Any, CultureInfo.InvariantCulture, out var val))
			{
				min = max = val;
				return true;
			}
			return false;
		}
		if (decimal.TryParse(parts[0], NumberStyles.Any, CultureInfo.InvariantCulture, out var minVal))
			min = minVal;
		if (decimal.TryParse(parts[^1], NumberStyles.Any, CultureInfo.InvariantCulture, out var maxVal))
			max = maxVal;
		return min != null || max != null;
	}

	private bool FilterDecimal(decimal value, string filter)
	{
		if (TryParseDecimalRange(filter, out var min, out var max))
		{
			if (min != null && value < min.Value) return false;
			if (max != null && value > max.Value) return false;
			return true;
		}
		// fallback to substring match
		return value.ToString(CultureInfo.InvariantCulture).Contains(filter, StringComparison.OrdinalIgnoreCase);
	}
}

public class BondViewModel
{
	public string? Name { get; set; }
	public decimal Coupon { get; set; }
	public DateTime MaturityDate { get; set; }
	public string? Epic { get; set; }
	public decimal DirtyPrice { get; set; }
	public decimal CurrentYield { get; set; }
	public decimal AccruedInterest { get; set; }
	public decimal Convexity { get; set; }
	public decimal ModifiedDuration { get; set; }
	public decimal PresentValueOverDirty { get; set; }
	public decimal YieldToMaturity { get; set; }
	public decimal? OfferQty { get; set; }

	public object? GetPropertyValue(string? prop)
	{
		if (string.IsNullOrEmpty(prop)) return null;
		var pi = typeof(BondViewModel).GetProperty(prop);
		return pi?.GetValue(this);
	}
}
