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
STUDENTS (fields: list of names, surname, semester, code, list of classes)<br/>
TEACHERS (fields: list of names, surname, rank, code, list of classes)<br/>
ROOMS (fields: number, type, list of classes)<br/>
CLASSES (fields: name, code, duration, list of students, list of teachers)<br/>

# Available Commands
LIST     <COLLECTION> <br/>
FIND     <COLLECTION> <FIELD = VALUE> <br/>
EDIT     <COLLECTION> <FIELD = VALUE> <br/>
ADD      <CLASS> <BASE|SECONDARY> <br/>
DELETE   <CLASS> <FIELD = VALUE> <br/>
UNDO <br/>
REDO <br/>
HISTORY <br/>
QUEUE MODE: <br/>
        -QUEUE ON <br/>
        -QUEUE PRINT <br/>
        -QUEUE COMMIT <br/>
        -QUEUE DISMISS <br/>
        -QUEUE OFF <br/>
EXIT <br/>

# Design Patterns
- Adapter <br/>
- Command <br/>
- Iterator <br/>
- Factory <br/>
