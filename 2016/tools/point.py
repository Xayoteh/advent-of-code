from typing import Self


class Point():
    def __init__(self, x: int = 0, y: int = 0) -> None:
        self.x = x
        self.y = y

    def copy(self) -> "Point":
        return Point(self.x, self.y)

    def __iadd__(self, point: "Point") -> Self:
        self.x += point.x
        self.y += point.y
        return self

    def __add__(self, point: "Point") -> "Point":
        return Point(self.x + point.x, self.y + point.y)

    def __imul__(self, mult: int) -> Self:
        self.x *= mult
        self.y *= mult
        return self

    def __mul__(self, mult: int) -> "Point":
        return Point(self.x * mult, self.y * mult)

    def __str__(self) -> str:
        return f"({self.x}, {self.y})"

    def __hash__(self) -> int:
        return hash((self.x, self.y))

    def __eq__(self, point: "Point") -> bool:
        return point and self.x == point.x and self.y == point.y


class Directions():
    UP = Point(0, -1)
    DOWN = Point(0, 1)
    LEFT = Point(-1, 0)
    RIGHT = Point(1, 0)

    WITHOUT_D = [UP, RIGHT, DOWN, LEFT]
