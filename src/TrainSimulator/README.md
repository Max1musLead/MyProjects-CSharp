﻿# Симулятор транспортной системы поездов на магнитной подушке

## Описание проекта

Разработка объектной модели и программной реализации симулятора транспортной системы поездов на магнитной подушке. Проект предназначен для проверки корректности работы автоматизированной транспортной системы, снижения эксплуатационных расходов и предотвращения возможных инцидентов.

## Ключевые особенности

### Моделирование поездов
- **Атрибуты**: масса, скорость, ускорение, максимально допустимая сила.
- **Функциональность**:
    - Расчет времени прохождения дистанции с использованием физических законов.
    - Управление ускорением и скоростью через приложенные силы.

### Типы участков маршрута
1. **Обычные пути**: расчет времени прохождения на основе длины и скорости.
2. **Силовые пути**: применение сил для ускорения или замедления поезда.
3. **Станции**: посадка и высадка пассажиров, контроль скорости прибытия.

### Маршруты
- Набор участков, включающих различные типы путей и станций.
- Контроль завершения маршрута с учетом допустимых скоростных лимитов.

### Обработка результатов
- Результат прохождения маршрута: **успех** или **неудача**.
- Расчет общего времени прохождения маршрута.

## Технологии и методология

- **Язык программирования**: C#.
- **Принципы разработки**: инкапсуляция, композиция, полиморфизм, соблюдение основных принципов ООП.
- **Качество кода**:
    - Иммутабельность маршрутов и их компонентов.
    - Исключение использования исключений для обработки бизнес-логики.
- **Тестирование**:
    - Покрытие модульными тестами.
    - Реализация 8 тестовых сценариев с успешным прохождением.

## Результаты

- Создана модель транспортной системы, демонстрирующая корректность работы маршрутов.
- Реализация полностью соответствует функциональным и нефункциональным требованиям.
- Решение готово к масштабированию и использованию в реальных проектах.

Проект демонстрирует использование передовых подходов объектно-ориентированного программирования, инженерной методологии и высокого уровня тестирования для решения сложных прикладных задач.
