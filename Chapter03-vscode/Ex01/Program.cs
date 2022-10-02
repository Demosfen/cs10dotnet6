using static System.Console;

/*

1. What happens when you divide an int variable by 0? - (Ошибка, на ноль делить нельзя)
2. What happens when you divide a double variable by 0? - (Ошибка, на ноль делить нельзя)
3. What happens when you overflow an int variable, that is, set it to a value beyond its range? - (
    error CS0266: Не удается неявно преобразовать тип "long" в "int". Существует явное преобразование (возможно, пропущено приведение
 типов).
)

4. What is the difference between x = y++; and x = ++y;? 0 (Очевидно, в первом случае Х будет на 1 меньше У, во втором, Х = У)

5. What is the difference between break, continue, and return when used inside a loop statement?
    (break - прерывает цикл; continue - пропускает текущую итерацию в loop; return - завершает функцию и возвращает значение функции)

6. What are the three parts of a for statement and which of them are required?
    •An initializer expression, which executes once at the start of the loop.
    • A conditional expression, which executes on every iteration at the start of the loop to
    check whether the looping should continue.
    • An iterator expression, which executes on every loop at the bottom of the statement

7. What is the difference between the = and == operators?
    (Первый - оператор присваивания, второй - сравнения)

8. Does the following statement compile? for ( ; true; ) ;
    (Да, бесконечный цикл - зацикливание)

9. What does the underscore _ represent in a switch expression?
    (Default)

10. What interface must an object implement to be enumerated over by using the foreach statement?
    1.  Тип должен иметь метод GetEnumerator, который возвращает объект.
    2.  Возвращаемый объект должен иметь свойство Current и метод MoveNext.
    3.  Метод MoveNext должен возвращать значение true, если есть другие элементы для перечисления, или значение false, если элементов больше нет.
         */
