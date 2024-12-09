import sys

from PyQt5.QtGui import QFont
from PyQt5.QtWidgets import QApplication, QWidget, QPushButton, QLabel

command = ""


def button_press(arg):
    global command
    command += str(arg)
    txt.setText(command)


def add_button(n, numb = None):
    global number_buttons
    if numb == None:
        number_buttons[n].clicked.connect(lambda: button_press(9 - n if n != 10 else 0))
    else:
        action_buttons[n].clicked.connect(lambda: button_press(numb))


def get_result():
    global command

    txt.setText(str(eval(command)))
    command = str(eval(command))

def clear_input():
    global command

    command = command[:-1]
    txt.setText(command)

if __name__ == '__main__':
    app = QApplication(sys.argv)

    w = QWidget()
    w.resize(240, 300)
    w.setWindowTitle('Calculator')

    txt = QLabel(command, w)
    txt.setFont(QFont('Arial', 20))
    txt.move(10, 15)
    txt.resize(220, 30)

    # Building buttons
    number_buttons = []

    number = 0
    row = 0
    col = 0

    size = 60

    for number in range(0, 9):
        number_buttons.append(QPushButton(str(9 - number), w))
        add_button(number)
        number_buttons[number].resize(size, size)
        number_buttons[number].move(col*size, row*size + size)
        if (number+1) % 3 == 0 and number != 0:
            row += 1
            col = 0
        else:
            col += 1

    number_buttons.append(QPushButton("0", w))
    add_button(9)
    number_buttons[9].resize(size, size)
    number_buttons[9].move(size, 4 * size)

    # Action buttons
    action_buttons = []
    action_buttons.append(QPushButton("+", w))
    add_button(0, "+")
    action_buttons[0].resize(size, size)
    action_buttons[0].move(size * 3, size)

    action_buttons.append(QPushButton("-", w))
    add_button(1, "-")
    action_buttons[1].resize(size, size)
    action_buttons[1].move(size * 3, size * 2)

    action_buttons.append(QPushButton("*", w))
    add_button(2, "*")
    action_buttons[2].resize(size, size)
    action_buttons[2].move(size * 3, size * 3)

    action_buttons.append(QPushButton("/", w))
    add_button(3, "/")
    action_buttons[3].resize(size, size)
    action_buttons[3].move(size * 3, size * 4)

    action_buttons.append(QPushButton("=", w))
    action_buttons[4].clicked.connect(lambda: get_result())
    action_buttons[4].resize(size, size)
    action_buttons[4].move(size * 2, size * 4)

    action_buttons.append(QPushButton("<", w))
    action_buttons[5].clicked.connect(clear_input)
    action_buttons[5].resize(size, size)
    action_buttons[5].move(0, size*4)

    w.show()

    sys.exit(app.exec_())
