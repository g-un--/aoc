import crypto from 'crypto';

export function findHashWithNumberOfZeros(input: string, zeros: number) : string {
    let number = 0;
    let found = false;

    while (!found) {
        const inputData = `${input}${number}`;
        const hash = crypto.createHash('md5');
        const data = hash.update(inputData);
        const genHash = data.digest('hex');
        if(genHash.startsWith("0".repeat(zeros))) {
            found = true;
            break;
        }
        number += 1;
    }
    
    return number.toString();
}

export function part1(input: string) : string {
    return findHashWithNumberOfZeros(input, 5);
}

export function part2(input: string) : string {
    return findHashWithNumberOfZeros(input, 6);
}
