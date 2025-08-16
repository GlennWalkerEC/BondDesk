using BondDesk.BondProvider;
using BondDesk.DateTimeProvider;
using BondDesk.Domain.Interfaces.Services;
using BondDesk.GiltsInIssueRepo;
using BondDesk.QuoteProvider;
using System.Globalization;


namespace BondDesk.WinApp;

public partial class BondDesk : Form
{
	private readonly IGiltsService _giltsService;
	private List<BondViewModel> _bonds = new();
	private bool _sortAscending = true;
	private string? _sortColumn = null;
	private ToolTip _bondToolTip = new ToolTip(); // Add this as a field

	public BondDesk()
	{
		_giltsService = new GiltsService(new CachedRepo(), new Gilts(), new SimpleDateTimeProvider());
		InitializeComponent();

		// Start maximized
		WindowState = FormWindowState.Maximized;

		// Wire up all filter TextBoxes in new order
		_filterEpicTextBox.TextChanged += (s, e) => UpdateBondPanels();
		_filterNameTextBox.TextChanged += (s, e) => UpdateBondPanels();
		_filterMaturityDateTextBox.TextChanged += (s, e) => UpdateBondPanels();
		_filterAccruedInterestTextBox.TextChanged += (s, e) => UpdateBondPanels();
		_filterCouponTextBox.TextChanged += (s, e) => UpdateBondPanels();
		_filterYieldToMaturityTextBox.TextChanged += (s, e) => UpdateBondPanels();
		_filterCurrentYieldTextBox.TextChanged += (s, e) => UpdateBondPanels();
		_filterDirtyPriceTextBox.TextChanged += (s, e) => UpdateBondPanels();
		_filterPresentValueOverDirtyTextBox.TextChanged += (s, e) => UpdateBondPanels();
		_filterModifiedDurationTextBox.TextChanged += (s, e) => UpdateBondPanels();
		_filterDV1KGBPTextBox.TextChanged += (s, e) => UpdateBondPanels();
		_filterPresentValueTextBox.TextChanged += (s, e) => UpdateBondPanels();

		Load += Form1_Load;
		Resize += (s, e) => { ResizeFilterControls(); UpdateBondPanels(); };

		ApplyFilterSortTheme();
		ResizeFilterControls();

		Text = $"Bond Desk - {_giltsService.AssumedReinvestmentRate.ToString("P4")}";
	}

	private async void Form1_Load(object? sender, EventArgs e)
	{
		await UpdateDataAsync();
		UpdateBondPanels();
	}

	private async Task UpdateDataAsync()
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
				DV1KGBP = bond.DV1KGBP,
				ModifiedDuration = bond.ModifiedDuration,
				PresentValueOverDirty = bond.PresentValueOverDirty,
				YieldToMaturity = bond.YieldToMaturity,
				PresentValue = bond.PresentValue,
				ToolTipText = bond.ToString()
			});
		}
	}

	private async Task RefreshDataAsync()
	{
		await UpdateDataAsync();
		UpdateBondPanels();
	}

	protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
	{
		if (keyData == Keys.F5)
		{
			_ = RefreshDataAsync();
			return true;
		}
		return base.ProcessCmdKey(ref msg, keyData);
	}

	private void ApplyFilterSortTheme()
	{
		var filterBg = System.Drawing.Color.FromArgb(30, 34, 36);
		var filterFg = System.Drawing.Color.White;
		var filterFont = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular);

		// New order of filter textboxes
		_filterEpicTextBox.PlaceholderText = "Epic";
		_filterNameTextBox.PlaceholderText = "Name";
		_filterMaturityDateTextBox.PlaceholderText = "Maturity";
		_filterAccruedInterestTextBox.PlaceholderText = "Accrued";
		_filterCouponTextBox.PlaceholderText = "Coupon %";
		_filterYieldToMaturityTextBox.PlaceholderText = "YTM %";
		_filterCurrentYieldTextBox.PlaceholderText = "Yield %";
		_filterDirtyPriceTextBox.PlaceholderText = "Dirty";
		_filterPresentValueOverDirtyTextBox.PlaceholderText = "PV/Dirty";
		_filterModifiedDurationTextBox.PlaceholderText = "Modified Duration";
		_filterDV1KGBPTextBox.PlaceholderText = "DV1KGBP";
		_filterPresentValueTextBox.PlaceholderText = "Present Value";

		TextBox[] filters = {
			_filterEpicTextBox, _filterNameTextBox, _filterMaturityDateTextBox, _filterAccruedInterestTextBox, _filterCouponTextBox,
			_filterYieldToMaturityTextBox, _filterCurrentYieldTextBox, _filterDirtyPriceTextBox, _filterPresentValueOverDirtyTextBox,
			_filterModifiedDurationTextBox, _filterDV1KGBPTextBox, _filterPresentValueTextBox
		};
		foreach (var tb in filters)
		{
			tb.BackColor = filterBg;
			tb.ForeColor = filterFg;
			tb.Font = filterFont;
			tb.BorderStyle = BorderStyle.FixedSingle;
		}
		_filterSortPanel.BackColor = filterBg;
	}

	private void ResizeFilterControls()
	{
		// Layout filter and sort controls inside _filterSortPanel
		int totalFilters = 12; // Changed from 13 to 12 to match actual filter count
		int spacing = 8;
		int left = 12;
		int availableWidth = _filterSortPanel.ClientSize.Width - (left * 2) - (spacing * (totalFilters - 1));
		int boxWidth = availableWidth / totalFilters;
		int top = 12;
		TextBox[] filters = {
			 // New order of filter textboxes
			_filterEpicTextBox, _filterNameTextBox, _filterMaturityDateTextBox, _filterAccruedInterestTextBox, _filterCouponTextBox,
			_filterYieldToMaturityTextBox, _filterCurrentYieldTextBox, _filterDirtyPriceTextBox, _filterPresentValueOverDirtyTextBox,
			_filterModifiedDurationTextBox, _filterDV1KGBPTextBox, _filterPresentValueTextBox
		};
		for (int i = 0; i < filters.Length; i++)
		{
			filters[i].Left = left + i * (boxWidth + spacing);
			filters[i].Top = top;
			filters[i].Width = boxWidth;
			filters[i].Height = 28;
		}
	}

	private bool StringFilterMatches(string? value, string filter)
	{
		if (string.IsNullOrWhiteSpace(filter)) return true;
		var terms = filter.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
		if (terms.Length == 0) return true;
		if (string.IsNullOrEmpty(value)) return false;
		foreach (var term in terms)
		{
			if (value.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0)
				return true;
		}
		return false;
	}

	private void UpdateBondPanels()
	{
		var darkBg = System.Drawing.Color.FromArgb(20, 24, 26);
		var panelBg = System.Drawing.Color.FromArgb(30, 34, 36);
		// Color scheme by data type
		var textString = System.Drawing.Color.FromArgb(0, 255, 0); // Epic, Name
		var textDate = System.Drawing.Color.FromArgb(255, 255, 0); // Maturity
		var textDecimal = System.Drawing.Color.FromArgb(255, 140, 0); // Accrued, Coupon, YTM, Yield, Dirty, PV/Dirty, OfferQty, ModDur, DV1K, DayChangePc, PresentValue
		var textHeader = System.Drawing.Color.White;
		var textPV = System.Drawing.Color.White;
		var font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular);
		var underline = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Underline);
		var headerFont = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold);

		_bondsPanel.SuspendLayout();
		_bondsPanel.Controls.Clear();
		_bondToolTip.RemoveAll(); // Clear previous tooltips
		var filtered = _bonds.Where(b =>
			StringFilterMatches(b.Epic, _filterEpicTextBox.Text) &&
			StringFilterMatches(b.Name, _filterNameTextBox.Text) &&
			(string.IsNullOrWhiteSpace(_filterMaturityDateTextBox.Text) || b.MaturityDate.ToString("yyyy-MM-dd").Contains(_filterMaturityDateTextBox.Text, StringComparison.OrdinalIgnoreCase)) &&
			(string.IsNullOrWhiteSpace(_filterAccruedInterestTextBox.Text) || FilterDecimal(b.AccruedInterest, _filterAccruedInterestTextBox.Text)) &&
			(string.IsNullOrWhiteSpace(_filterCouponTextBox.Text) || FilterDecimal(b.Coupon, _filterCouponTextBox.Text, 100.0)) &&
			(string.IsNullOrWhiteSpace(_filterYieldToMaturityTextBox.Text) || FilterDecimal(b.YieldToMaturity, _filterYieldToMaturityTextBox.Text, 100.0)) &&
			(string.IsNullOrWhiteSpace(_filterCurrentYieldTextBox.Text) || FilterDecimal(b.CurrentYield, _filterCurrentYieldTextBox.Text, 100.0)) &&
			(string.IsNullOrWhiteSpace(_filterDirtyPriceTextBox.Text) || FilterDecimal(b.DirtyPrice, _filterDirtyPriceTextBox.Text)) &&
			(string.IsNullOrWhiteSpace(_filterPresentValueOverDirtyTextBox.Text) || FilterDecimal(b.PresentValueOverDirty, _filterPresentValueOverDirtyTextBox.Text)) &&
			(string.IsNullOrWhiteSpace(_filterModifiedDurationTextBox.Text) || FilterDecimal(b.ModifiedDuration, _filterModifiedDurationTextBox.Text)) &&
			(string.IsNullOrWhiteSpace(_filterDV1KGBPTextBox.Text) || FilterDecimal(b.DV1KGBP, _filterDV1KGBPTextBox.Text)) && 
			(string.IsNullOrWhiteSpace(_filterPresentValueTextBox.Text) || FilterDecimal(b.PresentValue, _filterPresentValueTextBox.Text))
		).ToList();

		if (!string.IsNullOrEmpty(_sortColumn))
		{
			filtered = _sortAscending
				? filtered.OrderBy(b => b.GetPropertyValue(_sortColumn)).ToList()
				: filtered.OrderByDescending(b => b.GetPropertyValue(_sortColumn)).ToList();
		}

		int panelWidth = _bondsPanel.ClientSize.Width - 10;
		int labelCount = 12;
		int labelSpacing = 8;
		int labelWidth = (panelWidth - (labelSpacing * (labelCount - 1))) / labelCount;

		// New column order and display names
		(string prop, string text, System.Drawing.Color color)[] headers = new[]
		{
			("Epic", "Epic", textString),
			("Name", "Name", textString),
			("MaturityDate", "Maturity", textDate),
			("AccruedInterest", "Accrued", textDecimal),
			("Coupon", "Coupon %", textDecimal),
			("YieldToMaturity", "YTM %", textDecimal),
			("CurrentYield", "Yield %", textDecimal),
			("DirtyPrice", "Dirty", textDecimal),
			("PresentValueOverDirty", "NPV/Dirty", textDecimal),
			("ModifiedDuration", "Modified Duration", textDecimal),
			("DV1KGBP", "DV1KGBP", textDecimal),
			("PresentValue", "Present Value", textPV)
		};
		// Header row
		var headerPanel = new Panel { Height = 25, Dock = DockStyle.Top, Width = panelWidth, Margin = new Padding(3), BackColor = darkBg };
		int headerLeft = 0;
		for (int i = 0; i < headers.Length; i++)
		{
			var (prop, text, color) = headers[i];
			string sortSymbol = _sortColumn == prop ? (_sortAscending ? " ^" : " v") : "";
			var label = new Label
			{
				Text = text + sortSymbol,
				Left = headerLeft,
				Top = 8,
				Width = labelWidth,
				AutoSize = false,
				ForeColor = color,
				Font = headerFont,
				BackColor = darkBg,
				TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
				Cursor = Cursors.Hand
			};
			label.Click += (s, e) => OnHeaderLabelClick(prop);
			headerPanel.Controls.Add(label);
			headerLeft += labelWidth + labelSpacing;
		}
		_bondsPanel.Controls.Add(headerPanel);

		var maxCoupon = filtered.Any() ? filtered.Min(x => x.Coupon) : 0m;
		var maxYtm = filtered.Any() ? filtered.Max(x => x.YieldToMaturity) : 0m;
		var maxModDuration = filtered.Any() ? filtered.Min(x => x.ModifiedDuration) : 0m;
		var maxPvDirty = filtered.Any() ? filtered.Max(x => x.PresentValueOverDirty) : 0m;
		var maxYield = filtered.Any() ? filtered.Max(x => x.CurrentYield) : 0m;
		var maxDV1K = filtered.Any() ? filtered.Max(x => x.DV1KGBP) : 0m;
		var minDirty = filtered.Any() ? filtered.Min(x => x.DirtyPrice) : 0m;
		var minAcc = filtered.Any() ? filtered.Min(x => x.AccruedInterest) : 0m;

		foreach (var bond in filtered)
		{
			var panel = new Panel { Height = 50, Dock = DockStyle.Top, Width = panelWidth, Margin = new Padding(3), BackColor = panelBg };
			int left = 0;
			Label[] labels = new Label[]
			{
				new Label
				{
					Text = bond.Epic,
					Width = labelWidth,
					AutoSize = false,
					ForeColor = textString,
					Font = font,
					BackColor = panelBg,
					TextAlign = System.Drawing.ContentAlignment.MiddleCenter
				},
				new Label
				{
					Text = bond.Name,
					Width = labelWidth,
					AutoSize = false,
					ForeColor = textString,
					Font = font,
					BackColor = panelBg,
					TextAlign = System.Drawing.ContentAlignment.MiddleCenter
				},
				new Label
				{
					Text = bond.MaturityDate.ToString("yyyy-MM-dd"),
					Width = labelWidth,
					AutoSize = false,
					ForeColor = textDate,
					Font = font,
					BackColor = panelBg,
					TextAlign = System.Drawing.ContentAlignment.MiddleCenter
				},
				new Label
				{
					Text = bond.AccruedInterest.ToString("C4", CultureInfo.CurrentCulture),
					Width = labelWidth,
					AutoSize = false,
					ForeColor = textDecimal,
					Font = bond.AccruedInterest == minAcc ? underline : font,
					BackColor = panelBg,
					TextAlign = System.Drawing.ContentAlignment.MiddleCenter
				},
				new Label
				{
					Text = (bond.Coupon * 100).ToString("F4", CultureInfo.InvariantCulture) + "%",
					Width = labelWidth,
					AutoSize = false,
					ForeColor = textDecimal,
					Font = bond.Coupon == maxCoupon ? underline : font,
					BackColor = panelBg,
					TextAlign = System.Drawing.ContentAlignment.MiddleCenter
				},
				new Label
				{
					Text = (bond.YieldToMaturity * 100).ToString("F4", CultureInfo.InvariantCulture) + "%",
					Width = labelWidth,
					AutoSize = false,
					ForeColor = textDecimal,
					Font = bond.YieldToMaturity == maxYtm ? underline : font,
					BackColor = panelBg,
					TextAlign = System.Drawing.ContentAlignment.MiddleCenter
				},
				new Label
				{
					Text = (bond.CurrentYield * 100).ToString("F4", CultureInfo.InvariantCulture) + "%",
					Width = labelWidth,
					AutoSize = false,
					ForeColor = textDecimal,
					Font = bond.CurrentYield == maxYield ? underline : font,
					BackColor = panelBg,
					TextAlign = System.Drawing.ContentAlignment.MiddleCenter
				},
				new Label
				{
					Text = bond.DirtyPrice.ToString("C4", CultureInfo.CurrentCulture),
					Width = labelWidth,
					AutoSize = false,
					ForeColor = textDecimal,
					Font = bond.DirtyPrice == minDirty ? underline : font,
					BackColor = panelBg,
					TextAlign = System.Drawing.ContentAlignment.MiddleCenter
				},
				new Label
				{
					Text = bond.PresentValueOverDirty.ToString("F4", CultureInfo.InvariantCulture),
					Width = labelWidth,
					AutoSize = false,
					ForeColor = textDecimal,
					Font = bond.PresentValueOverDirty == maxPvDirty ? underline : font,
					BackColor = panelBg,
					TextAlign = System.Drawing.ContentAlignment.MiddleCenter
				},
				new Label
				{
					Text = bond.ModifiedDuration.ToString("F4", CultureInfo.InvariantCulture),
					Width = labelWidth,
					AutoSize = false,
					ForeColor = textDecimal,
					Font = bond.ModifiedDuration == maxModDuration ? underline : font,
					BackColor = panelBg,
					TextAlign = System.Drawing.ContentAlignment.MiddleCenter
				},
				new Label
				{
					Text = bond.DV1KGBP.ToString("C4", CultureInfo.CurrentCulture),
					Width = labelWidth,
					AutoSize = false,
					ForeColor = textDecimal,
					Font = bond.DV1KGBP == maxDV1K ? underline : font,
					BackColor = panelBg,
					TextAlign = System.Drawing.ContentAlignment.MiddleCenter
				},
				new Label
				{
					Text = bond.PresentValue.ToString("F4", CultureInfo.InvariantCulture),
					Width = labelWidth,
					AutoSize = false,
					ForeColor = textPV,
					Font = font,
					BackColor = panelBg,
					TextAlign = System.Drawing.ContentAlignment.MiddleCenter
				}
			};
			for (int i = 0; i < labels.Length; i++)
			{
				labels[i].Left = left;
				labels[i].Top = 8;
				panel.Controls.Add(labels[i]);
				_bondToolTip.SetToolTip(labels[i], bond.ToolTipText);
				left += labelWidth + labelSpacing;
			}
			_bondsPanel.Controls.Add(panel);
		}
		_bondsPanel.ResumeLayout();
	}

	private void OnHeaderLabelClick(string prop)
	{
		if (_sortColumn == prop)
		{
			_sortAscending = !_sortAscending;
		}
		else
		{
			_sortColumn = prop;
			_sortAscending = true;
		}
		UpdateBondPanels();
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

	private bool FilterDecimal(decimal value, string filter, double multiplier = 1.0)
	{
		if (TryParseDecimalRange(filter, out var min, out var max))
		{
			decimal adjValue = (decimal)((double)value * multiplier);
			if (min != null && adjValue < min.Value) return false;
			if (max != null && adjValue > max.Value) return false;
			return true;
		}
		// fallback to substring match using raw string
		string formatted = ((double)value * multiplier).ToString(CultureInfo.InvariantCulture);
		return formatted.Contains(filter, StringComparison.OrdinalIgnoreCase);
	}
}

public class BondViewModel
{
	public string ToolTipText { get; set; }

	public string? Name { get; set; }
	public decimal Coupon { get; set; }
	public DateTime MaturityDate { get; set; }
	public string? Epic { get; set; }
	public decimal DirtyPrice { get; set; }
	public decimal CurrentYield { get; set; }
	public decimal AccruedInterest { get; set; }
	public decimal DV1KGBP { get; set; }
	public decimal ModifiedDuration { get; set; }
	public decimal PresentValueOverDirty { get; set; }
	public decimal YieldToMaturity { get; set; }
	public decimal PresentValue { get; set; }

	public object? GetPropertyValue(string? prop)
	{
		if (string.IsNullOrEmpty(prop)) return null;
		var pi = typeof(BondViewModel).GetProperty(prop);
		return pi?.GetValue(this);
	}
}
