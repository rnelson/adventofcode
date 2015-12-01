! http://adventofcode.com/day/1
! 
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

program puzzle01
    implicit none

    integer, parameter :: infile = 10
    character(7000) :: text
    character(1) :: ch
    character(8) :: num
    integer :: fl, i

    ! Open the text file and read all 7,000 characters
    open(unit=infile, file='inputs/input01.txt')
    read(infile, '(a7000)') text

    fl = 0

    do i = 1, 7000
        ch = text(i:i)
        if (ch == '(') then
            fl = fl + 1
        elseif (ch == ')') then
            fl = fl - 1
        end if
    end do

    write (num, '(i8)') fl
    print '(a8)', adjustl(num)
end program

