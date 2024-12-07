use std::fs;

fn main() {
	println!("AoC 2022 - Day 1: Calorie Counting");

	let input = fs::read_to_string("../../aoc-inputs/2022/input.txt").expect("cannot read input file");
    let lines: Vec<&str> = input.lines().collect();
    
    let mut calories: Vec<u32> = vec![];
    let mut sums: Vec<u32> = vec![];

    for line in lines {
        println!("[{}]", line);

        if line.is_empty() {
            // todo: store sum
            calories = vec![];
        } else {
            println!("<{}>", line);

            let number = line.parse::<u32>().expect("could not parse");
            calories.push(number);

            println!("{}", number);
        }
    }
}