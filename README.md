# Balancer
Simple program for recalculating debets between a group of people

Input: 
List of transactions who pays, how much, for whom

Output:
List of transactions who pays, how much, for whom

Algorithm:
1. Caculate the account state for each person
2. Create two list of people
    - The first list containing people with plus account state
    - The second list containing people with minus account state
3. Sort lists
    - The first list in descending order
    - The second list in ascending order
5. Take the first person from the first [Person A] and the second list [Person B]
6. Calculate new account states for the people:
    - If debt is less or equal then the plus state
      - Person B should pay all debt to Person A
      - Remove Person B from the list
      - Update Person A plus state [Current state - debt of Person B]
        - If Person A has 0 balance
          - Remove Person A from the list
    - If debt is more then the plus state
      - Person B should pay to Person A the amount of Person A plus state 
      - Remove Person A from the list
      - Update Person B deb state [Current state - Person A plus state]
  7. Repeat from step 3 until lists are not empty
 8. Done
