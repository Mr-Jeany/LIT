import random
import sys

from PyQt5.QtCore import Qt
from PyQt5.QtWidgets import QApplication, QWidget, QGridLayout, QPushButton, QVBoxLayout, QLabel, QMainWindow

#
# Комментарии здесь есть не везде, так как я сначала их писал, а потом жестко перехотел
#


class GameLogic:
    def __init__(self):
        self.score = 0
        self.size = 4 # размер поля

        # делаем пустое поле
        self.board = [[0] * self.size for _ in range(self.size)]
        self._set_new_tile()
        self._set_new_tile()

    def _set_new_tile(self):
        # делаем список с координатами пустых полей
        empty_tiles = [(y, x) for y in range(self.size) for x in range(self.size) if self.board[y][x] == 0]

        # получаем случайное значение. 2 = 90%, 4 = 10%
        random_value = 2 if random.random() < 0.9 else 4

        # и ставим в клетку
        chosen_tile = random.choice(empty_tiles)
        self.board[chosen_tile[0]][chosen_tile[1]] = random_value

        self.score += random_value

    def move(self, m):
        possible = self._check_if_still_possible_moves(m)
        while self._check_if_still_possible_moves(m):
            for y in range(self.size):
                for x in range(self.size):
                    c_y = y
                    c_x = x
                    value = self.board[y][x]
                    if value != 0:
                        if m == "down":
                            while self._check_if_move_possible(c_y, c_x, m):
                                self.board[c_y][c_x] = 0
                                if self.board[c_y + 1][c_x] != 0:
                                    self.board[c_y + 1][c_x] += value
                                    break
                                else:
                                    self.board[c_y + 1][c_x] += value
                                c_y += 1

                        elif m == "up":
                            while self._check_if_move_possible(c_y, c_x, m):
                                self.board[c_y][c_x] = 0
                                if self.board[c_y - 1][c_x] != 0:
                                    self.board[c_y - 1][c_x] += value
                                    break
                                else:
                                    self.board[c_y - 1][c_x] += value
                                c_y -= 1

                        elif m == "left":
                            while self._check_if_move_possible(c_y, c_x, m):
                                self.board[c_y][c_x] = 0
                                if self.board[c_y][c_x - 1] != 0:
                                    self.board[c_y][c_x - 1] += value
                                    break
                                else:
                                    self.board[c_y][c_x - 1] += value
                                c_x -= 1

                        elif m == "right":
                            while self._check_if_move_possible(c_y, c_x, m):
                                self.board[c_y][c_x] = 0
                                if self.board[c_y][c_x + 1] != 0:
                                    self.board[c_y][c_x + 1] += value
                                    break
                                else:
                                    self.board[c_y][c_x + 1] += value
                                c_x += 1

        if possible:
            self._set_new_tile()

        return self._check_if_still_possible_moves()

    def _check_if_still_possible_moves(self, m="any"):
        for y in range(self.size):
            for x in range(self.size):
                if self._check_if_move_possible(y, x, m):
                    return True

        return False

    def _check_if_move_possible(self, y, x, move):
        if move == "any":
            return (y < 3 and (self.board[y][x] == self.board[y + 1][x] or self.board[y + 1][x] == 0)) or (y > 0 and (self.board[y][x] == self.board[y - 1][x] or self.board[y - 1][x] == 0)) or (x > 0 and (self.board[y][x] == self.board[y][x - 1] or self.board[y][x - 1] == 0)) or (x < 3 and (self.board[y][x] == self.board[y][x + 1] or self.board[y][x + 1] == 0)) and self.board[y][x] != 0
        elif move == "down":
            return y < 3 and (self.board[y][x] == self.board[y + 1][x] or self.board[y + 1][x] == 0) and self.board[y][x] != 0
        elif move == "up":
            return y > 0 and ((self.board[y][x] == self.board[y - 1][x]) or self.board[y - 1][x] == 0) and self.board[y][x] != 0
        elif move == "left":
            return x > 0 and (self.board[y][x] == self.board[y][x - 1] or self.board[y][x - 1] == 0) and self.board[y][x] != 0
        elif move == "right":
            return x < 3 and (self.board[y][x] == self.board[y][x + 1] or self.board[y][x + 1] == 0) and self.board[y][x] != 0




class Game(QMainWindow):
    def __init__(self):
        super().__init__()
        # ставим настройки окна крч и добавляем логику игры
        self.setWindowTitle("\"2048\" - Game")
        self.setFixedSize(400, 600)
        self.game = GameLogic()

        # делаем layout
        self.central_widget = QWidget()
        self.setCentralWidget(self.central_widget)
        self.layout = QVBoxLayout(self.central_widget)

        # добавляем score
        self.score_label = QLabel(f"Score: {self.game.score}", self)
        self.score_label.setAlignment(Qt.AlignCenter)
        self.layout.addWidget(self.score_label)

        # сетка - самое важное тут, ибо будет позволять там размещать клетки
        self.grid_widget = QWidget()
        self.grid_layout = QGridLayout(self.grid_widget)
        self.grid_layout.setSpacing(10)
        self.layout.addWidget(self.grid_widget)

        self.tiles = [[QLabel(self) for _ in range(self.game.size)] for _ in range(self.game.size)]
        for y in range(self.game.size):
            for x in range(self.game.size):
                self.tiles[y][x].setFixedSize(80, 80)
                self.tiles[y][x].setStyleSheet("background: #ffffff; font-size: 24px; border-radius: 5px;")
                self.tiles[y][x].setAlignment(Qt.AlignCenter)
                self.grid_layout.addWidget(self.tiles[y][x], y, x)
        self.update_grid()

        # кнопки управления
        self.up_button = QPushButton("Up", self)
        self.up_button.clicked.connect(lambda: self.move("up"))
        self.layout.addWidget(self.up_button)

        self.down_button = QPushButton("Down", self)
        self.down_button.clicked.connect(lambda: self.move("down"))
        self.layout.addWidget(self.down_button)

        self.left_button = QPushButton("Left", self)
        self.left_button.clicked.connect(lambda: self.move("left"))
        self.layout.addWidget(self.left_button)

        self.right_button = QPushButton("Right", self)
        self.right_button.clicked.connect(lambda: self.move("right"))
        self.layout.addWidget(self.right_button)

        # кнопка ресета
        self.restart_button = QPushButton("Restart", self)
        self.restart_button.clicked.connect(self.restart_game)
        self.layout.addWidget(self.restart_button)
        self.restart_button.setStyleSheet("background: #8a130b; color: #ffffff;")

    def update_grid(self):
        for y in range(self.game.size):
            for x in range(self.game.size):
                v = self.game.board[y][x]
                self.tiles[y][x].setText(str(v) if v != 0 else "")
                self.tiles[y][x].setStyleSheet(self.styles(v))

        self.score_label.setText(f"Score: {self.game.score}")


    def restart_game(self):
        self.game = GameLogic()
        self.update_grid()

    def move(self, m):
        self.update_grid()
        if not self.game.move(m):
            self.score_label.setText(f"Game over! Score: {self.game.score}")




    @staticmethod
    def styles(value):
        # тут просто получаем стили для кнопок, в зависимости от значения (value)
        tiles_styles = {
            0: "background: #cdc1b4; font-size: 24px; color: #776e65; border-radius: 5px;",
            2: "background: #eee4da; font-size: 24px; color: #776e65; border-radius: 5px;",
            4: "background: #ede0c8; font-size: 24px; color: #776e65; border-radius: 5px;",
            8: "background: #f2b179; font-size: 24px; color: #f9f6f2; border-radius: 5px;",
            16: "background: #f59563; font-size: 24px; color: #f9f6f2; border-radius: 5px;",
            32: "background: #f67c5f; font-size: 24px; color: #f9f6f2; border-radius: 5px;",
            64: "background: #f65e3b; font-size: 24px; color: #f9f6f2; border-radius: 5px;",
            128: "background: #edcf72; font-size: 20px; color: #f9f6f2; border-radius: 5px;",
            256: "background: #edcc61; font-size: 20px; color: #f9f6f2; border-radius: 5px;",
            512: "background: #edc850; font-size: 20px; color: #f9f6f2; border-radius: 5px;",
            1024: "background: #edc53f; font-size: 18px; color: #f9f6f2; border-radius: 5px;",
            2048: "background: #edc22e; font-size: 18px; color: #f9f6f2; border-radius: 5px;",
        }
        # используем .get, чтобы поставить дефолт значения для value > 2048
        return tiles_styles.get(value, "background: #3c3a32; font-size: 18px; color: #f9f6f2; border-radius: 5px;")


if __name__ == '__main__':
    app = QApplication(sys.argv)
    window = Game()
    window.show()
    sys.exit(app.exec_())
