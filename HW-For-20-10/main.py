import sqlite3

db_connect = sqlite3.connect('database.db')
cursor = db_connect.cursor()

commands_info = {"help": "Return info about all commands",
                 "new (student, teacher, course, exam, grade)": "Adds new value to the table",
                 "edit (student, teacher, course)": "Updates value in the table",
                 "delete (student, teacher, course, exam) [id]": "Deletes value from the table",
                 "get_course_by_teacher": "Get all courses with teacher with specific id",
                 "get_students_by_department": "Get all students with specific department",
                 "get_students_by_course": "Get all students on specific course",
                 "get_grades_by_course": "Get all grades on specific course",
                 "average_of_student_on_course": "Get average grade of a student on specific course",
                 "average_of_student": "Get average grade of a student",
                 "average_of_dep": "Get average grade of a department",
                 "exit": "Exit the program"}

working = True


def execute_command(cmd):
    global working
    cmd = cmd.split()
    main_part = cmd[0]
    if main_part == 'help': # Help command
        print("\n- Help -")
        for i in commands_info:
            print(f"{i} - {commands_info[i]}")

    elif main_part == 'exit': # Exit command
        print("\nExiting the program... Goodbye!")
        working = False

    elif main_part == 'new': # 1. Добавление нового студента, преподавателя, курса, экзамена и оценки.
        print(f"\n- Creating new {cmd[1]} -")

        if cmd[1] == "student":
            print("Enter their info (name, surname, department, date_of_birth) separating values by \", \":", end=" ")
            new_info = input().split(", ")
            execute = """INSERT INTO Students (name, surname, department, date_of_birth) VALUES (?, ?, ?, ?)"""

        elif cmd[1] == "teacher":
            print("Enter their info (name, surname, department) separating values by \", \":", end=" ")
            new_info = input().split(", ")
            execute = """INSERT INTO Teachers (name, surname, department) VALUES (?, ?, ?)"""

        elif cmd[1] == "course":
            print("Enter its info (title, description, teacher_id) separating values by \", \":", end=" ")
            new_info = input().split(", ")
            execute = """INSERT INTO Courses (title, description, teacher_id) VALUES (?, ?, ?)"""

        elif cmd[1] == "exam":
            print("Enter its info (date, max_score, course_id) separating values by \", \":", end=" ")
            new_info = input().split(", ")
            execute = """INSERT INTO Exams (date, max_score, course_id) VALUES (?, ?, ?)"""

        elif cmd[1] == "grade":
            print("Enter its info (score, student_id, exam_id) separating values by \", \":", end=" ")
            new_info = input().split(", ")
            execute = """INSERT INTO Grades (score, student_id, exam_id) VALUES (?, ?, ?)"""

        else:
            print("Invalid arguments for \"new\" command")
            return

        cursor.execute(execute, new_info)
        db_connect.commit()
        print(f"New {cmd[1]} created!")

    elif main_part == "edit": # 2. Изменение информации о студентах, преподавателях и курсах.
        print(f"\n- Editing {cmd[1]} -")
        if cmd[1] == "student":
            print("First enter their ID, then their info (name, surname, department, date_of_birth) separating values by \", \":",end=" ")
            new_info = input().split(", ")
            id = new_info[0]
            new_info.pop(0)
            new_info.append(id)
            execute = """UPDATE Students SET name = ?, surname = ?, department = ?, date_of_birth = ? WHERE id = ?"""

        elif cmd[1] == "teacher":
            print("First enter their ID, then their info (name, surname, department) separating values by \", \":", end=" ")
            new_info = input().split(", ")
            id = new_info[0]
            new_info.pop(0)
            new_info.append(id)
            execute = """UPDATE Teachers SET name = ?, surname = ?, department = ? WHERE id = ?"""

        elif cmd[1] == "course":
            print("First enter its ID, then its info (title, description, teacher_id) separating values by \", \":", end=" ")
            new_info = input().split(", ")
            id = new_info[0]
            new_info.pop(0)
            new_info.append(id)
            execute = """UPDATE Teachers SET title = ?, description = ?, teacher_id = ? WHERE id = ?"""

        else:
            print("Invalid arguments for \"edit\" command")
            return

        cursor.execute(execute, new_info)
        db_connect.commit()
        print(f"Edited {cmd[1]}")

    elif main_part == "delete": # 3. Удаление студентов, преподавателей, курсов и экзаменов.
        print(f"- Deleting {cmd[1]} -")
        execute = f"""DELETE FROM {cmd[1].title() + "s"} WHERE id = {int(cmd[2])}"""
        cursor.execute(execute)
        db_connect.commit()
        print(f"Deleted {cmd[1]}")

    elif main_part == 'get_students_by_department': # 4. Получение списка студентов по факультету
        crs = input("\n- Insert department you would like to search for: ")
        execute = f"""
        SELECT * FROM Students WHERE Department = \"{crs}\"
        """
        cursor.execute(execute)
        data = cursor.fetchall()
        print(f"\n- All students on department \"{crs}\" -")
        for row in data:
            print(f"id: {row[0]} | name: {row[1]} | surname: {row[2]} | department: {row[3]} | date_of_birth: {row[4]}")

    elif main_part == 'get_course_by_teacher': # 5. Получение списка курсов, читаемых определенным преподавателем.
        crs = input("\n- Insert teacher id you would like to search for: ")
        execute = f"""
        SELECT * FROM Courses WHERE teacher_id = \"{int(crs)}\"
        """
        cursor.execute(execute)
        data = cursor.fetchall()
        print(f"\n- All courses with teacher with id \"{crs}\" -")
        for row in data:
            print(f"id: {row[0]} | title: {row[1]} | description: {row[2]} | teacher_id: {row[3]}")

    elif main_part == 'get_students_by_course': # 6. Получение списка студентов, зачисленных на конкретный курс.
        crs = input("\n- Insert course id you would like to search for: ")
        execute = f"""
        SELECT DISTINCT Students.name, Students.surname, Exams.course_id FROM Grades
        JOIN Students on Students.id = Grades.student_id
        JOIN Exams on Grades.exam_id = Exams.id WHERE Exams.course_id = {int(crs)}
        """
        cursor.execute(execute)
        data = cursor.fetchall()
        print(f"\n- All courses with teacher with id \"{crs}\" -")
        for row in data:
            print(f"Students.name: {row[0]} | Students.surname: {row[1]} | Exams.course_id: {row[2]}")

    elif main_part == 'get_grades_by_course': # 7. Получение оценок студентов по определенному курсу.
        crs = input("\n- Insert course id you would like to search for: ")
        execute = f"""
        SELECT Grades.id, Grades.score, Students.name, Students.surname, Exams.course_id FROM Grades
        JOIN Students on Students.id = Grades.student_id
        JOIN Exams on Grades.exam_id = Exams.id WHERE Exams.course_id = {int(crs)}
        """
        cursor.execute(execute)
        data = cursor.fetchall()
        print(f"\n- All courses with teacher with id \"{crs}\" -")
        for row in data:
            print(f"Grades.id: {row[0]} | Grades.score: {row[1]} | Students.name: {row[2]} | Students.surname: {row[3]} | Exams.course_id: {row[4]}")

    elif main_part == "average_of_student_on_course": # 8. Средний балл студента по определенному курсу
        crs = input("\n- Insert course id you would like to search for: ")
        std = input("\n- Insert student id you would like to search for: ")
        execute = f"""
        SELECT Grades.student_id, Grades.id, Grades.score, Exams.course_id FROM Grades
        JOIN Exams on Grades.exam_id = Exams.id WHERE Exams.course_id = {int(crs)} AND Grades.student_id = {int(std)}
        """
        cursor.execute(execute)
        data = cursor.fetchall()
        counter = 0
        summ = 0
        for row in data:
            counter += 1
            summ += row[2]
        print(f"Result: {summ / counter}")

    elif main_part == "average_of_student": # 9. Средний балл студента в целом.
        std = input("\n- Insert student id you would like to search for: ")
        execute = f"""
        SELECT Grades.student_id, Grades.id, Grades.score, Exams.course_id FROM Grades
        JOIN Exams on Grades.exam_id = Exams.id WHERE Grades.student_id = {int(std)}
        """
        cursor.execute(execute)
        data = cursor.fetchall()
        counter = 0
        summ = 0
        for row in data:
            counter += 1
            summ += row[2]
        print(f"Result: {summ / counter}")

    elif main_part == "average_of_dep": # 10. Средний балл по факультету.
        std = input("\n- Insert department you would like to search for: ")
        print(std)
        execute = f"""
        SELECT Grades.student_id, Grades.id, Grades.score, Exams.course_id FROM Grades
        JOIN Exams on Grades.exam_id = Exams.id 
        JOIN Students on Grades.student_id = Students.id WHERE Students.department = \"{std}\"
        """
        cursor.execute(execute)
        data = cursor.fetchall()
        counter = 0
        summ = 0
        for row in data:
            counter += 1
            summ += row[2]
        print(f"Result: {summ / counter}")

    else:
        print("UNKNOWN COMMAND")

while working:
    print("\n- Available commands -")
    print("help | exit | get_students_by_department | get_course_by_teacher | get_students_by_course | get_grades_by_course")
    print("average_of_student_on_course | average_of_student | average_of_dep")
    print("new (student, teacher, course, exam, grade) | edit (student, teacher, course) | delete (student, teacher, course, exam) [id] ")
    execute_command(input("> "))
