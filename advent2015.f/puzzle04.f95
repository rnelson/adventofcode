! http://adventofcode.com/day/4
! 
! Part 1
! ------
! Santa needs help mining some AdventCoins (very similar to
! bitcoins) to use as gifts for all the economically
! forward-thinking little girls and boys.
! 
! To do this, he needs to find MD5 hashes which, in hexadecimal,
! start with at least five zeroes. The input to the MD5 hash
! is some secret key (your puzzle input, given below) followed by
! a number in decimal. To mine AdventCoins, you must find Santa
! the lowest positive number (no leading zeroes: 1, 2, 3, ...)
! that produces such a hash.
! 
! For example:
! 
!  - If your secret key is abcdef, the answer is 609043, because
!       the MD5 hash of abcdef609043 starts with five zeroes
!       (000001dbbfa...), and it is the lowest such number to do so.
!  - If your secret key is pqrstuv, the lowest number it combines
!       with to make an MD5 hash starting with five zeroes is
!       1048970; that is, the MD5 hash of pqrstuv1048970 looks
!       like 000006136ef....
! 
! 
! Part 2
! ------
! Now find one that starts with six zeroes.

include 'md5.f95'

program puzzle04
    implicit none

    interface
        function work(input, prefix, digest)
            implicit none
            character(len=*), intent(in) :: input, prefix
            character(len=32), intent(out) :: digest
            integer work
        end function work
    end interface

    integer, parameter :: infile = 10
    character(10000) :: text
    character(50) :: input
    character(32) :: digest
    character(30) :: num
    integer :: strlen, i, j, digestlen
    logical :: found

    open(unit=infile, file='../advent2015/inputs/input04.txt')

    ! Read the input file
    read(infile, '(a10000)') text
    text = trim(text)
    strlen = len(trim(text))
    
    ! Part 1
    i = work(trim(text), '00000', digest)
    write (num, '(i8)') i
    print '(a22, a8, a10, a32)', '[Fortran] Puzzle 4-1: ', trim(adjustl(num)), ' produces ', trim(digest)

    ! Part 2
    i = work(trim(text), '000000', digest)
    write (num, '(i8)') i
    print '(a22, a8, a10, a32)', '[Fortran] Puzzle 4-2: ', adjustl(num), ' produces ', digest
end program

integer function work(text, prefix, digest)
    implicit none

    interface
        function check(digest, prefix)
            implicit none
            character(len=*), intent(in) :: digest, prefix
            logical check
        end function check
        function md5(string)
            implicit none
            character*(*), intent(in) :: string
            character*32 md5
        end function md5
    end interface

    character(len=*), intent(in) :: text, prefix
    character(len=32), intent(out) :: digest
    character(len=50) :: input
    character(len=30) :: num
    integer :: inputlen, prefixlen, i, j
    logical :: found

    found = .false.
    i = -1
    do while (.not. found)
        i = i + 1

        write (num, '(i30)') i
        input = trim(trim(text) // trim(adjustl(num)))
        digest = md5(trim(input))

        ! The MD5 algorithm has a known issue where 0s become spaces, so
        ! we need to change them back for the comparison to work
        do j = 1, 32
            if (digest(j:j) == ' ') then
                digest(j:j) = '0'
            end if
        end do

        found = check(digest, prefix)
    end do

    work = i
end function

logical function check(digest, prefix)
    implicit none
    character(len=*), intent(in) :: digest, prefix
    integer :: prefixlen, i

    prefixlen = len(prefix)

    if (digest(1:prefixlen) == prefix) then
        check = .true.
    else
        check = .false.
    end if
end function
