using UIKit;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using WebMolen.Mobile.Core.Views;
using AlcmariaVictrix.App.iOS;

[assembly: ExportRenderer(typeof(TextCellExtended), typeof(TextCellExtendedRenderer))]
namespace AlcmariaVictrix.App.iOS
{
    class TextCellExtendedRenderer : TextCellRenderer
    {
        private UIView _bgView;

        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);

            var textCellExtended = item as TextCellExtended;

            if (textCellExtended.ShowDisclosure)
                cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;

            if (textCellExtended.SelectedBackgroundColor != default(Color))
            {
                if (_bgView == null)
                {
                    _bgView = new UIView(cell.SelectedBackgroundView.Bounds);
                    _bgView.Layer.BackgroundColor = textCellExtended.SelectedBackgroundColor.ToCGColor();
                }

                cell.SelectedBackgroundView = _bgView;
            }

            return cell;
        }
    }
}