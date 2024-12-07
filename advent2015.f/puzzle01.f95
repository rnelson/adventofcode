! http://adventofcode.com/day/1
!
! Part 1
! ------
! Santa is trying to deliver presents in a large apartment building, but he
! can't find the right floor - the directions he got are a little confusing.
! He starts on the ground floor (floor 0) and then follows the instructions
! one character at a time.
! 
! An opening parenthesis, (, means he should go up one floor, and a closing
! parenthesis, ), means he should go down one floor.
! 
! The apartment building is very tall, and the basement is very deep; he will
! never find the top or bottom floors.
! 
! For example:
! 
!   - (()) and ()() both result in floor 0.
!   - ((( and (()(()( both result in floor 3.
!   - ))((((( also results in floor 3.
!   - ()) and ))( both result in floor -1 (the first basement level).
!   - ))) and )())()) both result in floor -3.
! 
! To what floor do the instructions take Santa?
! 
! 
! Part 2
! ------
! Now, given the same instructions, find the position of the first character
! that causes him to enter the basement (floor -1). The first character in the
! instructions has position 1, the second character has position 2, and so on.
! 
! For example:
! 
!   - ) causes him to enter the basement at character position 1.
!   - ()()) causes him to enter the basement at character position 5.
! 
! What is the position of the character that causes Santa to first enter the
! basement?

program puzzle01
    implicit none

    integer, parameter :: infile = 10
    character(7000) :: text
    character(1) :: ch
    character(8) :: num
    integer :: fl, pos, i
    logical :: found

    ! Open the text file and read all 7,000 characters
    open(unit=infile, file='../../aoc-inputs/2015/input01.txt')
    read(infile, '(a7000)') text

    fl = 0
    pos = 0
    found = .false.

    do i = 1, 7000
        ch = text(i:i)
        if (ch == '(') then
            fl = fl + 1
        elseif (ch == ')') then
            fl = fl - 1
        end if
        
        if (.not. found) then
            pos = pos + 1
            
            if (fl < 0) then
                found = .true.
            end if
        end if
    end do

    write (num, '(i8)') fl
    print '(a22, a8)', '[Fortran] Puzzle 1-1: ', adjustl(num)
    write (num, '(i8)') pos
    print '(a22, a8)', '[Fortran] Puzzle 1-2: ', adjustl(num)
end program

