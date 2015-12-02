! http://adventofcode.com/day/1
! 
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

program puzzle02
    implicit none

    integer, parameter :: infile = 10
    character(7000) :: text
    character(1) :: ch
    character(8) :: num
    integer :: fl, i

    ! Open the text file and read all 7,000 characters
    open(unit=infile, file='../advent2015/inputs/input02.txt')
    read(infile, '(a7000)') text

    fl = 0

    do i = 1, 7000
        ch = text(i:i)
        if (ch == '(') then
            fl = fl + 1
        elseif (ch == ')') then
            fl = fl - 1
        end if
        
        if (fl < 0) then
            write (num, '(i8)') i
            print '(a8)', adjustl(num)
            
            stop
        end if
    end do
end program
