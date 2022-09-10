/*
Задача 41: Пользователь вводит с клавиатуры M чисел. Посчитайте, сколько чисел больше 0 ввёл пользователь.

0, 7, 8, -2, -2 -> 2

1, -7, 567, 89, 223-> 3

Задача 43: Напишите программу, которая найдёт точку пересечения двух прямых, заданных уравнениями y = k1 * x + b1, y = k2 * x + b2; значения b1, k1, b2 и k2 задаются пользователем.

b1 = 2, k1 = 5, b2 = 4, k2 = 9 -> (-0,5; -0,5)
*/
bool more_or_less (double x) //Возвращает истину, есле x больше нуля
{
  if (x>0) {return true;} else {return false;} 
}
double input (int m) //Ввод с консоли числа под номером m
{
  Console.WriteLine($"Введите число номер_{m} = "); 
  double x = Convert.ToDouble (Console.ReadLine());  
  return x;
}
double kx_b (double k, double b, double x ) //Функция y=kx+b
{
  return (k * x + b);
}
int sgn (double a) //Функция определения знака (1 если a>0)
{
  if (a==0) {return 0;}
  if (a>0) {return 1;} else {return -1;}
}

//Функция, рекурсивно приближающая определение координаты х точки пересечения функций с заданной точностью dх0
//(по умолчанию dх0=0.0001) "мечась" то вправо, то влево. Начальное значение dx берется большим (по умолчанию 100)
//для колоссального сокращения числа операций.
//Код не оптимизирован для наглядности !!!!!

double Priblij (double xz, double dx, double k1, double b1, double k2, double b2, double dx0)
{
  double xz_p = xz;
  double xz_m = xz;

  do 
  {
    double sgn_p = sgn( kx_b(k1,b1,xz_p) - kx_b(k2,b2,xz_p) );            
    double sgn_p_dx = sgn( kx_b(k1,b1,xz_p+dx) - kx_b(k2,b2,xz_p+dx) );   

    if ( sgn_p != sgn_p_dx )
    {
      
      xz = xz_p+dx/2;                                                     
      dx=dx/10;                                                           
      xz = Priblij (xz, dx , k1,b1,k2,b2,dx0);

    } else {xz_p=xz_p+dx;}

    double sgn_m = sgn( kx_b(k1,b1,xz_m) - kx_b(k2,b2,xz_m) );            
    double sgn_m_dx = sgn( kx_b(k1,b1,xz_m-dx) - kx_b(k2,b2,xz_m-dx) );   

    if ( sgn_m != sgn_m_dx )
    {
      xz = xz_m-dx/2;                                                     
      dx=dx/10;                                                           
      xz = Priblij (xz, dx, k1,b1,k2,b2,dx0);

    } else {xz_m=xz_m-dx;}
  }
  while (dx>dx0);

  return xz;
}

//--------------------------------------------Program body----------------------------------------------


Console.WriteLine("Введите номер задачи: 41 или 43:");
int num = Convert.ToInt32(Console.ReadLine());

switch (num)
{

case 41: 
    {
        Console.WriteLine("Введите число M:");
        Console.Write("M= "); int M = Convert.ToInt32(Console.ReadLine());
           
        int more=0;
        int ch=0; 
        do
        {
          if ( more_or_less ( input (ch+1) ) ) {more++;}
          ch++;
        } while (ch < M);

        Console.Write($"Количество чисел больше ноля = {more}"); 

        break;    
    } //Zadacha 41

case 43:
    {
        Console.Write("k1= "); double k1 = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("Введите число b1:");
        Console.Write("b1= "); double b1 = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("Введите число k2:");
        Console.Write("k2= "); double k2 = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("Введите число b2:");
        Console.Write("b2= "); double b2 = Convert.ToDouble(Console.ReadLine());

        double x_trive = (b2-b1)/(k1-k2); // Тривиальное решение задачи
        double y_trive = kx_b(k1, b1, x_trive);

        Console.WriteLine($"Значение Х, вычисленное тривиальным способом = {x_trive}");
        Console.WriteLine($"Значение y, вычисленное тривиальным способом = {y_trive}"); 


        double x_nontrive = Priblij (0,100, k1,b1,k2,b2, 0.0001); //Решение задачи методом рекурсии
        double y_nontrive = kx_b(k1, b1, x_nontrive);

        Console.WriteLine($"Значение Х, вычисленное способом рекурсии = {x_nontrive}");
        Console.WriteLine($"Значение y, вычисленное способом рекурсии = {y_nontrive}");

        break;
    } //Zadacha 43
 
}