using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Layouts;

namespace RubiksCube;

public partial class PageCube : ContentPage
{
    public PageCube()
    {
        InitializeComponent();

        //var polygon = new Polygon
        //{
        //    Points = new PointCollection { new Point(0, 0), new Point(0, 1), new Point(1, 0) },
        //    Fill = Brush.Red
        //};

        //var absoluteLayout = new AbsoluteLayout();
        //AbsoluteLayout.SetLayoutBounds(polygon, new Rectangle(new Point(0, 0), new Size(1, 1)));
        //AbsoluteLayout.SetLayoutFlags(polygon, AbsoluteLayoutFlags.All);
        //absoluteLayout.Children.Add(polygon);

        //Content = absoluteLayout;
    }

    private void OnColorDrop(object sender, DropEventArgs e)
    {

    }
}
