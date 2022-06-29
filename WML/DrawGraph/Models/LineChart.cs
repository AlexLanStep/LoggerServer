

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DrawGraph.Models;

public abstract class Chart
{
  // Коэффициент высоты графика относительно высоты контейнера.
  private readonly double _factor = 0.666666666666667;
  protected readonly double PaddingChart = 10;
  protected double WidthChart;
  protected double HeightChart;
  public readonly Canvas ChartBackground = new();

  public Chart()
  {
    ChartBackground.Margin = new Thickness(2);
    ChartBackground.SizeChanged += ChartBackground_SizeChanged;
  }

  private void ChartBackground_SizeChanged(object sender, SizeChangedEventArgs e)
  {
    // Инициализация ширины и высоты графиков.
    WidthChart = e.NewSize.Width - (PaddingChart * 2);

    // Верхний предел графика на 10 линии снизу.
    HeightChart = e.NewSize.Height * _factor;

    // Закрашиваем задний фон графика рисованной кистью.
    ChartBackground.Background = DrawLines(e.NewSize.Width, WidthChart, PaddingChart);
  }

  public abstract void AddValue(double value);
  public abstract void Clear();



  private Brush DrawLines(double actualwidth, double widthchart, double padding)
  {
    double W = actualwidth;
    double w = widthchart;
    double offset = padding;

    // --- Величины для относительных расчетов ---

    // W - (абсолютная величина как общий знаменатель)
    // (Относительная ширина контейнера графика (рисуется сетка заднего фона)) = 1 (W/W)

    // x - (Относительная ширина поля графика) = w/W
    double x = w / W;

    // delta - (Относительное смещение сетки заднего фона графика) = offset/W
    double delta = offset / W;

    // --- 

    // Количество линий по горизонтали и вертикали.
    // По вертикали всего 15 линий, но график только до 10-ой.
    int numLines = 10;

    DrawingBrush brush = new()
    {
      // Режим задающий правило заполнения фона элемента плитками 
      TileMode = TileMode.Tile,

      // Область просмотра задана относительными величинами.
      // График будет иметь в высоту и в ширину одинаковое кол-во линий.
      Viewport = new Rect(delta, 0, x / numLines, _factor / numLines),

      // Рисуем прямоугольник, формирующий фоновую сетку.
      Drawing = new GeometryDrawing()
      {
        Pen = new(Brushes.Black, 0.05),
        Brush = new SolidColorBrush(Color.FromRgb(240, 240, 240)),
        Geometry = new RectangleGeometry(new Rect(0, 0, 45, 20))
      }
    };

    return brush;
  }

}


public class LineChart : Chart
  {
      private readonly double _lineThickness = 4;
      private readonly double _sizePoint = 20;


      public override void AddValue(double value)
      {
          // Получаем все значения которые уже есть в графике.
          List<double> listValues = ChartBackground.Children.OfType<Ellipse>().Select(p => (double)p.Tag).ToList();

          // Вычисляем новую длину отрезка полилинии, чтобы график поместился 
          // полностью на ширину поля.
          double lengthSectionLine = listValues.Count > 0 
                                      ? WidthChart / listValues.Count 
                                      : WidthChart;

          // Добавляем новое значение в график.
          listValues.Add(value);

          // Для ограничения высоты графика, вне зависимости от абсолютных значений,
          // вычислим общий знаменатель. И самое большое значение будет на максимальной
          // допустимой высоте. остальные пропорционально ниже.
          double maxValue = listValues.Max();

          double denominator = maxValue / (ChartBackground.ActualHeight-25);        //HeightChart;

          // Удалим текущие элементы графика.
          Clear();

          // Инициализация новой ломаной линии.
          Polyline _polyline = new();
          _polyline.Stroke = Brushes.BlueViolet;
          _polyline.StrokeThickness = _lineThickness;
          _polyline.StrokeDashCap = PenLineCap.Flat;
          _polyline.StrokeLineJoin = PenLineJoin.Round;
          _polyline.HorizontalAlignment = HorizontalAlignment.Left;
          ChartBackground.Children.Add(_polyline);


          // Создание графика по текущим абсолютным значениям.
          // Абсолютные значения сохраняются в свойствах Ellipse.Tag
          foreach (double val in listValues)
          {
              // Счётчик добавленных в график узловых точек.
              int count = ChartBackground.Children.OfType<Ellipse>().Count();

              // Относительная высота точки от нижнего края.
              // Для этого все абсолютные значения делятся на общий знаменатель,
              // чтобы максимальная высота точек не выходила выше установленной.
              double heightPoint = val / denominator;

              // Координаты узловой точки.
              double x = (count * lengthSectionLine) + (ChartBackground.ActualWidth - WidthChart) / 2;
              double y = heightPoint;

              // Узловая точка графика.
              Ellipse point = CreatePoint(x, y, _sizePoint, val);
              ChartBackground.Children.Add(point);

              // Надпись около узловой точки.
              Label title = CreateTitle(x - (_sizePoint / 2), y, val);
              ChartBackground.Children.Add(title);

              // Отрезок линии соединяющий предыдущую и текущую узловую точку.
              _polyline.Points.Add(new Point(x, ChartBackground.ActualHeight - y /* переворачиваем значение: отсчёт идёт от bottom*/));
          }
      }


      public override void Clear() => ChartBackground.Children.Clear();


      #region Private


      /// <summary>
      /// Создание узловой точки графика.
      /// </summary>
      /// <param name="x">x координата</param>
      /// <param name="y">y координата</param>
      /// <param name="value">абсолютное значение точки</param>
      /// <returns></returns>
      private Ellipse CreatePoint(double x, double y, double diameter, double value)
      {
          Random random = new();

          Ellipse point = new()
          {
              StrokeThickness = 2,
              Width = diameter,
              Height = diameter,
              Fill = new SolidColorBrush(Color.FromArgb(255, (byte)random.Next(0, 256), (byte)random.Next(0, 256), (byte)random.Next(0, 256))),
              Stroke = Brushes.White,
              Tag = value
          };

          Canvas.SetLeft(point, x - diameter / 2);
          // Отсчёт координат графика идёт от нижнего края.
          Canvas.SetBottom(point, y - diameter / 2);

          return point;
      }


      /// <summary>
      /// Создание текстовой надписи около узловой точки.
      /// </summary>
      /// <param name="x">x координата</param>
      /// <param name="y">y координата</param>
      /// <param name="value">абсолютное значение выводится как текст</param>
      /// <returns></returns>
      private Label CreateTitle(double x, double y, double value)
      {
          Label title = new()
          {
              Content = value,
              Padding = new Thickness(0, 0, 0, 10)
          };

          Canvas.SetLeft(title, x);
          // Отсчёт координат графика идёт от нижнего края.
          Canvas.SetBottom(title, y);

          return title;
      }


      #endregion
  }
