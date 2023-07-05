# Projektowanie_Obiektowe

The purpose of the project is to simulate a database of the University, storing information about students, teachers, rooms, and classes.
All the objects can be stored in different representations. 
It allows operations such as listing, finding, adding, and deleting objects that meet specific criteria.
It supports redoing and undoing commands and displaying command history. The project allows for exporting and importing executed commands.
It also offers a queue mode where commands are not automatically executed but added to a queue. You can print commands added to the queue, commit them or clear the queue. In this mode you can also export commands added to the queue or import commands.
In addition to the mentioned features, the project includes files containing implementations of collections: vector, list and tree. 
Using these collections you can add or delete objects in different representations. Iterators and reverse iterators are also implemented. 
Furthermore, the project also provides algorithms that leverage these collections for various purposes. 

# Collections
STUDENTS (fields: list of names, surname, semester, code, list of classes)
TEACHERS (fields: list of names, surname, rank, code, list of classes)
ROOMS (fields: number, type, list of classes)
CLASSES (fields: name, code, duration, list of students, list of teachers)

# Available Commands
LIST     <COLLECTION>
FIND     <COLLECTION> <FIELD = VALUE>
EDIT     <COLLECTION> <FIELD = VALUE>
ADD      <CLASS> <BASE|SECONDARY>
DELETE   <CLASS> <FIELD = VALUE>
UNDO
REDO
HISTORY
QUEUE MODE:
        -QUEUE ON
        -QUEUE PRINT
        -QUEUE COMMIT
        -QUEUE DISMISS
        -QUEUE OFF
EXIT

# Design Patterns
- Adapter
- Command
- Iterator
- Factory
