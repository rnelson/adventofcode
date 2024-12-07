! http://adventofcode.com/day/3
! 
! Part 1
! ------
! Santa is delivering presents to an infinite two-dimensional grid of
! houses.
! 
! He begins by delivering a present to the house at his starting
! location, and then an elf at the North Pole calls him via radio and
! tells him where to move next. Moves are always exactly one house to
! the north (^), south (v), east (>), or west (<). After each move, he
! delivers another present to the house at his new location.
! 
! However, the elf back at the north pole has had a little too much
! eggnog, and so his directions are a little off, and Santa ends up
! visiting some houses more than once. How many houses receive at least
! one present?
! 
! For example:
! 
!   - > delivers presents to 2 houses: one at the starting location,
!      and one to the east.
!   - ^>v< delivers presents to 4 houses in a square, including twice
!      to the house at his starting/ending location.
!   - ^v^v^v^v^v delivers a bunch of presents to some very lucky children
!      at only 2 houses.
! 
! 
! Part 2
! ------
! The next year, to speed up the process, Santa creates a robot version
! of himself, Robo-Santa, to deliver presents with him.
! 
! Santa and Robo-Santa start at the same location (delivering two presents
! to the same starting house), then take turns moving based on instructions
! from the elf, who is eggnoggedly reading from the same script as the previous
! year.
! 
! This year, how many houses receive at least one present?
! 
! For example:
! 
!   - ^v delivers presents to 3 houses, because Santa goes north, and then
!      Robo-Santa goes south.
!   - ^>v< now delivers presents to 3 houses, and Santa and Robo-Santa end
!      up back where they started.
!   - ^v^v^v^v^v now delivers presents to 11 houses, with Santa going one
!      direction and Robo-Santa going the other.

program puzzle03
    implicit none

    integer, parameter :: infile = 10
    character(10000) :: text
    character(8) :: num
    integer :: strlen, cnt, cnt2, i, j

    open(unit=infile, file='../../aoc-inputs/2015/input03.txt')

    ! Read the input file
    read(infile, '(a10000)') text
    text = trim(text)
    strlen = len(trim(text))
    
    ! Get all of our answers up front
    call counthouses(text, cnt, cnt2)
    
    ! Part 1
    write (num, '(i8)') cnt
    print '(a22, a8)', '[Fortran] Puzzle 3-1: ', adjustl(num)

    ! Part 2
    write (num, '(i8)') cnt2
    print '(a22, a8)', '[Fortran] Puzzle 3-2: ', adjustl(num)
end program

subroutine counthouses(route, cnt, cnt2)
    implicit none
    character(len=*), intent(in) :: route
    integer, intent(out) :: cnt, cnt2
    integer, dimension(-2000:2000, -2000:2000) :: houses
    integer :: strlen, currX, currY, prevX, prevY, i, j
    character :: ch
    
    strlen = len(trim(route))
    cnt = 0
    cnt2 = 0
    
    ! Initialize the entire array to 0
    do i = -2000, 2000
        do j = -2000, 2000
            houses(i, j) = 0
        end do
    end do
    
    ! Visit the starting house
    currX = 0
    currY = 0
    prevX = 0
    prevY = 0
    houses(prevX, prevY) = 1
    
    ! Visit all of the houses
    do i = 1, strlen
        ch = route(i:i)
        
        if (ch == '<') then
            currX = prevX - 1
            currY = prevY
        elseif (ch == '^') then
            currX = prevX
            currY = prevY + 1
        elseif (ch == '>') then
            currX = prevX + 1
            currY = prevY
        elseif (ch == 'v') then
            currX = prevX
            currY = prevY - 1
        end if
        
        houses(currX, currY) = 1
        prevX = currX
        prevY = currY
    end do
    
    ! Count the number of visited houses
    do i = -2000, 2000
        do j = -2000, 2000
            if (houses(i, j) == 1) then
                cnt = cnt + 1
            end if
        end do
    end do
    
    ! Initialize the entire array to 0 and do it all over
    ! again, this time for part 2
    do i = -2000, 2000
        do j = -2000, 2000
            houses(i, j) = 0
        end do
    end do
    
    ! Santa: Visit the starting house
    currX = 0
    currY = 0
    prevX = 0
    prevY = 0
    houses(prevX, prevY) = 1
    
    ! Santa: Visit all of the houses
    do i = 1, strlen, 2
        ch = route(i:i)
        
        if (ch == '<') then
            currX = prevX - 1
            currY = prevY
        elseif (ch == '^') then
            currX = prevX
            currY = prevY + 1
        elseif (ch == '>') then
            currX = prevX + 1
            currY = prevY
        elseif (ch == 'v') then
            currX = prevX
            currY = prevY - 1
        end if
        
        houses(currX, currY) = houses(currX, currY) + 1
        prevX = currX
        prevY = currY
    end do
    
    ! Robot Santa: Visit the starting house
    currX = 0
    currY = 0
    prevX = 0
    prevY = 0
    houses(prevX, prevY) = 1
    
    ! Robot Santa: Visit all of the houses
    do i = 2, strlen, 2
        ch = route(i:i)
        
        if (ch == '<') then
            currX = prevX - 1
            currY = prevY
        elseif (ch == '^') then
            currX = prevX
            currY = prevY + 1
        elseif (ch == '>') then
            currX = prevX + 1
            currY = prevY
        elseif (ch == 'v') then
            currX = prevX
            currY = prevY - 1
        end if
        
        houses(currX, currY) = houses(currX, currY) + 1
        prevX = currX
        prevY = currY
    end do
    
    ! Count the number of visited houses
    do i = -2000, 2000
        do j = -2000, 2000
            if (houses(i, j) > 0) then
                cnt2 = cnt2 + 1
            end if
        end do
    end do
end subroutine
