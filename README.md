# OS Task №3
Conveyor simulation program
## Short decription
Develop a program that simulates the operation of a warehouse (conveyor).

Given 3 producers and 2 consumers, all different threads and all work at the same time.

There is a queue with 200 elements. Producers add a random number from 1...100, and consumers take these numbers.

If there are >= 100 elements in the queue, the producers sleep, if there are no elements in the queue, the consumers sleep.

If items become <= 80 producers wake up.

All this works until the user presses the “q” button, after which the producers stop, and the consumers take all the elements, only then the program ends.
