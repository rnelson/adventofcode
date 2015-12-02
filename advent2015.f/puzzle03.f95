! http://adventofcode.com/day/2
! 
! The elves are also running low on ribbon. Ribbon is all the same width, so
! they only have to worry about the length they need to order, which they
! would again like to be exact.
! 
! The ribbon required to wrap a present is the shortest distance around its
! sides, or the smallest perimeter of any one face. Each present also
! requires a bow made out of ribbon as well; the feet of ribbon required for
! the perfect bow is equal to the cubic feet of volume of the present. Don't
! ask how they tie the bow, though; they'll never tell.
! 
! For example:
! 
!   -  A present with dimensions 2x3x4 requires 2+2+3+3 = 10 feet of ribbon to
!         wrap the present plus 2*3*4 = 24 feet of ribbon for the bow, for a
!         total of 34 feet.
!   -  A present with dimensions 1x1x10 requires 1+1+1+1 = 4 feet of ribbon to
!        wrap the present plus 1*1*10 = 10 feet of ribbon for the bow, for a
!        total of 14 feet.
! 
! How many total feet of ribbon should they order?

program puzzle03
    implicit none

    integer, parameter :: infile = 10
    character(50) :: text
    character(8) :: num
    integer :: total, length, width, height, ios
    integer :: area1, area2, area3, smallarea, s1, s2

    open(unit=infile, file='../advent2015/inputs/input03.txt')
    total = 0

    do
        ! Read the next line in
        read(infile, '(a50)', iostat=ios) text
        if (ios .ne. 0) then
            exit
        end if

        call tokenize(trim(text), length, width, height)
        call twosmallest(length, width, height, s1, s2)

        area1 = 2 * length * width
        area2 = 2 * width * height
        area3 = 2 * height * length
        smallarea = min(area1, area2, area3) / 2

        total = total + area1 + area2 + area3 + smallarea
    end do

    write (num, '(i8)') total
    print '(a8)', adjustl(num)
end program

subroutine tokenize(text, length, width, height)
    implicit none

    character(*), intent(in) :: text
    integer, intent(out) :: length, width, height
    character(10) :: values(3)
    integer :: pos1, pos2, n, i

    pos1 = 1
    pos2 = 0
    n = 0

    do
        pos2 = index(text(pos1:), 'x')

        if (pos2 == 0) then
            n = n + 1
            values(n) = text(pos1:)
            exit
        end if

        n = n + 1
        values(n) = text(pos1:pos1 + pos2 - 2)
        pos1 = pos2 + pos1
    end do

    read (values(1), '(i10)') length
    read (values(2), '(i10)') width
    read (values(3), '(i10)') height
end subroutine

subroutine twosmallest(val1, val2, val3, small1, small2)
    implicit none

    integer, intent(in) :: val1, val2, val3
    integer, intent(out) :: small1, small2
    integer :: big

    ! WARNING: ugly hack

    big = max(val1, val2, val3)

    if (big == val1) then
        small1 = val2
        small2 = val3
    elseif (big == val2) then
        small1 = val1
        small2 = val3
    else
        small1 = val1
        small2 = val2
    end if
end subroutine

