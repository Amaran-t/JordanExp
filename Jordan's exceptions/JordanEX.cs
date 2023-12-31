﻿using System;
using System.Text;

namespace JordansExceptions
{
    class JordanEX
    {
        public static void Main(string[] args)
        {
            UI ui = new UI();
            Console.WriteLine("\nВыберите метод:\n");
            Console.WriteLine("1) Метод Жордана;\n2) Метод искусственного базиса;\n3) Транспортная задача;\n");
            Console.Write("Выбор: ");
            int SelectedMethod = ui.CheckAndReadCorrectNumInput(3, 1);
            if (SelectedMethod == 1) // Метод Жордановых исключений
            {
                Console.WriteLine("\nМЕТОД ЖОРДАНОВЫХ ИСКЛЮЧЕНИЙ;\n");
                Console.Write("размерность системы или max n среди всех x^n: ");
                int MaxDegree = ui.CheckAndReadCorrectNumInput();
                Console.Write("количество уравнений в системе: ");
                int NumberEquation = ui.CheckAndReadCorrectNumInput();
                JordanMethod jordanMethod = new JordanMethod();
                // Массив параметров при x^n
                double[,] Matrix = new double[NumberEquation, MaxDegree];
                // Массив значений после равенства в системах
                double[] Answers = new double[NumberEquation];
                ui.FillArrayCoefficientsAndAnswersSystemEquationsAndCoofs(Matrix, Answers);
                bool[] CoefficientsNotTransferredYet = new bool[MaxDegree];
                Array.Fill(CoefficientsNotTransferredYet, true);
                string[] CoefficientsTransferred = new string[NumberEquation];
                Array.Fill(CoefficientsTransferred, "0 ");
                int[] CoordSelectedElem = new int[2];

                //Имеет единственное решение Х1 = 3; Х2 = 2; Х3 = 1
                //double[,] Matrix = { { 1, 3, -4 }, { -1, 1, 1 }, { 2, 1, 1 } };
                //double[] Answers = { 5, 0, 9 };

                //Имеет параметрическое решение Х1 = Z1; X2 = Z2; X3 = 4 - Z1 - 2 * Z2; X4 = 6 - Z1 - Z2; (Система совместна)
                //double[,] Matrix = { { 1, 2, 1, 0 }, { 1, 1, 0, 1 }, { 1, -1, -2, 3 } };
                //double[] Answers = { 4, 6, 10 };

                //Имеет параметрическое решение Х1 = 1 + Z1 - Z2; X2 = Z1; X3 = Z1 + Z2; X4 = Z2; (Количество уравнений больше чем степень)
                //double[,] Matrix = { { 1, 1, -2, 3 }, { 2, -1, -1, 3 } };
                //double[] Answers = { 1, 2 };

                string FinallyAnswer;
                while (true)
                {
                    ui.DrawJordanTable(Matrix, CoefficientsNotTransferredYet, CoefficientsTransferred);
                    CoordSelectedElem = ui.SelectJordanElem(Matrix, Answers, CoefficientsNotTransferredYet, CoefficientsTransferred);
                    jordanMethod.StepJordanEx(CoordSelectedElem[0], CoordSelectedElem[1], Matrix, Answers, CoefficientsNotTransferredYet, CoefficientsTransferred);
                    FinallyAnswer = jordanMethod.FindFinallyAnswer(Matrix, Answers, CoefficientsNotTransferredYet, CoefficientsTransferred);
                    if (FinallyAnswer == "nothing")
                    {
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("\n\nОтвет:\n" + FinallyAnswer);
                        break;
                    }
                }

            }
            else if (SelectedMethod == 2) // Сисплекс метод
            {
                Console.WriteLine("\nМЕТОД ИСКУССТВЕННОГО БАЗИСА;\n");
                Console.Write("размерность системы или max n среди всех x^n: ");
                int MaxDegree = ui.CheckAndReadCorrectNumInput();
                //int MaxDegree = 7;
                Console.Write("количество уравнений в системе: ");
                int NumberEquation = ui.CheckAndReadCorrectNumInput();
                //int NumberEquation = 5;
                SimplexMethod simplexMethod = new SimplexMethod(MaxDegree, NumberEquation);
                // Массив параметров при x^n
                double[,] Matrix = new double[NumberEquation, MaxDegree];
                // Массив значений после равенства в системах
                double[] Answers = new double[NumberEquation + 2];
                Array.Fill(Answers, 0);
                // Массив параметров при x^n функции
                double[] FCoofs = new double[MaxDegree];
                // Массив сумм элементов столбцов * (-1)
                double[] GCoofs = new double[MaxDegree];
                Array.Fill(GCoofs, 0);

                int[] IndependentElems = new int[MaxDegree];
                for (int i = 0; i < IndependentElems.Length; i++)
                {
                    IndependentElems[i] = i + 1;
                }

                Console.Write("\n\n");
                Console.Write("Это индексы верхних иксов");
                ui.MatrixWrite(IndependentElems);

                int[] DependentVariables = new int[NumberEquation];
                for (int i = 0; i < DependentVariables.Length; i++)
                {
                    DependentVariables[i] = i + 1 + IndependentElems.Length;
                }

                Console.Write("Это индексы левых иксов");
                ui.MatrixWrite(DependentVariables);

                // MOCKS
                //double[,] Matrix = {
                //    { 1, -4, 2, -5, 9 },
                //    { 0, 1, -3, 4, -5 },
                //    { 0, 1, -1, 1, -1 }
                //};
                //double[] Answers = { 3, 6, 1, 0, 0 };
                //double[] FCoofs = { 2, 6, -5, 1, 4 };

                //double[,] Matrix = {
                //    { 0, -1, 1, 1, 0 },
                //    { -5, 1, 1, 0, 0 },
                //    { -8, 1, 2, 0, -1 }
                //};
                //double[] Answers = { 1, 2, 3, 0, 0 };
                //double[] FCoofs = { 3, -1, -4, 0, 0 };

                // Задание на вторую лабу

                /*double[,] Matrix = {
                    { 1, 1, 1, 0, 0, 0, 0 },
                    { 1, -2, 0, 1, 0, 0, 0 },
                    { 2, 3, 0, 0, 1, 0, 0 },
                    { 3, 2, 0, 0, 0, 1, 0 },
                    { 2, 2, 0, 0, 0, 0, -1 },
                };
                double[] Answers = { 1, 1, 2, 3, 1, 0, 0 };
                double[] FCoofs = { 1, -1, 0, 0, 0, 0, 0 };*/
                // *****

                ui.FillArrayCoefficientsAndAnswersSystemEquationsAndCoofs(Matrix, Answers, FCoofs);
                simplexMethod.CalculationGCoofficients(Matrix, Answers, FCoofs, GCoofs);

                Console.Write("Это элементы Matrix");
                ui.MatrixWrite(Matrix);
                Console.Write("Это элементы Answers");
                ui.MatrixWrite(Answers);
                Console.Write("Это элементы строки F");
                ui.MatrixWrite(FCoofs);
                Console.Write("Это элементы строки G");
                ui.MatrixWrite(GCoofs);
                while (true)
                {
                    int[] IndexesResolvingElem = simplexMethod.FindIndexesResolvingElement(Matrix, Answers, FCoofs, GCoofs);
                    Console.Write("\n\n=================================\n");
                    Console.Write("Координаты разрешающего элемента");
                    ui.MatrixWrite(IndexesResolvingElem);
                    Console.Write("\n=================================\n\n");

                    if (IndexesResolvingElem[0] == -1 && IndexesResolvingElem[1] == -1)
                    {
                        Console.WriteLine("\n\n\nОтвет:\n\n\n");
                        double[] FinalAnswer = new double[NumberEquation + MaxDegree];

                        Array.Fill(FinalAnswer, -1);
                        for (int k = 0; k < IndependentElems.Length; k++)
                        {
                            FinalAnswer[IndependentElems[k] - 1] = 0;
                        }
                        for (int h = 0; h < DependentVariables.Length; h++)
                        {
                            FinalAnswer[DependentVariables[h] - 1] = Answers[h];
                        }

                        Console.Write("( ");
                        for (int t = 0; t < FinalAnswer.Length; t++)
                        {
                            Console.Write(FinalAnswer[t].ToString("0.##"));
                            if(t == FinalAnswer.Length - 1)
                            {
                                Console.Write(" ).\n\n");
                            }
                            else
                            {
                                Console.Write(" , ");
                            }
                        }
                        Console.Write(
                                $"F(x) = {Answers[Answers.Length - 2].ToString("0.##")}" +
                                $"{((Answers[Answers.Length - 1] == 0) ? " " : " -M * " + Answers[Answers.Length - 1].ToString("0.##"))}\n\n"
                                );
                        break;
                    }
                    else if (IndexesResolvingElem[0] == -2 && IndexesResolvingElem[1] == -2)
                    {
                        Console.WriteLine("\n\n\nРешений нет.\n\n\n");
                        break;
                    }
                    else if (IndexesResolvingElem[0] == -5 && IndexesResolvingElem[1] == -5)
                    {
                        Console.WriteLine("\n\n\nБред какой-то!\n\n\n");
                        break;
                    }
                    else if (IndexesResolvingElem[0] == -10 && IndexesResolvingElem[1] == -10)
                    {
                        Console.WriteLine("\n\n\nНичего не случилось?\n\n\n");
                        break;
                    }
                    else
                    {

                        simplexMethod.StepJordanEx(IndexesResolvingElem[0], IndexesResolvingElem[1], Matrix, Answers,
                                                            FCoofs, GCoofs, DependentVariables, IndependentElems);
                    }

                    Console.Write("\n\n=================================\n");

                    Console.Write("Это индексы верхних иксов");
                    ui.MatrixWrite(IndependentElems);

                    Console.Write("Это элементы Matrix");
                    ui.MatrixWrite(Matrix);

                    Console.Write("Это индексы левых иксов");
                    ui.MatrixWrite(DependentVariables);

                    Console.Write("Это элементы Answers");
                    ui.MatrixWrite(Answers);

                    Console.Write("Это элементы строки F");
                    ui.MatrixWrite(FCoofs);

                    Console.Write("Это элементы строки G");
                    ui.MatrixWrite(GCoofs);

                    Console.Write("\n=================================");
                    Console.Write("Нет Решения");
                    Console.ReadLine();
                }

            }
            else if (SelectedMethod == 3) // Транспортная задача
            {
                TransportTask transportTask = new TransportTask(ui);
                transportTask.TaskInit();
                //transportTask.TaskInitTest();
                transportTask.CheckIntermediateResult();
                while (true)
                {//Создание плана
                    Console.Write("\n\n******************************************************************\n\n");
                    transportTask.Step();
                    bool Result = transportTask.FindNextMinRate();//FindNextMinRateInternetVersion();
                    transportTask.CheckIntermediateResult();
                    if (Result)
                    {
                        break;
                    }
                }
                while (true)
                {//Оптимизация плана
                    Console.Write("\n\n******************************************************************\n");
                    Console.Write("******************************************************************\n\n");
                    Console.Write("Ф-ия F = " + transportTask.CalculateF() + ";\n\n");
                    transportTask.FindPotentials();
                    int[] IndexCell = transportTask.CheckPotentials();
                    if (IndexCell[0] == -1 && IndexCell[1] == -1)
                    {
                        break;
                    }
                    else
                    {
                        transportTask.FindAndUseContour(IndexCell);
                    }
                }
                Console.Write("\n\n******************************************************************\n");
                Console.Write("******************************************************************\n");
                Console.Write("******************************************************************\n\n");
                Console.Write("\nОТВЕТ:\n");
                Console.Write("\nФ-ия F = " + transportTask.CalculateF() + ";\n");
                transportTask.DrawFinalAnswer();
                Console.ReadLine();
            }

        }

    }
}