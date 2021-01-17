type House = {x: number; y:number};

function update(char: string, current: House): House {
    switch(char){
        case "^":
            current = {x: current.x, y: current.y + 1};
            break;
        case "v":
            current = {x: current.x, y: current.y - 1};
            break;
        case ">":
            current = {x: current.x + 1, y: current.y};
            break;
        case "<":
            current = {x: current.x - 1, y: current.y};
            break;
    }

    return current;
}

function getId({x, y}: House): string {
    return `${x}-${y}`;
}

export function part1(input: string) {
    const set = new Set<string>();
    let current = {x:0, y:0};
    set.add(getId(current));

    for(const char of input) {
        current = update(char, current);
        set.add(getId(current));
    }
    return set.size.toString();
}

export function part2(input: string) {
    const set = new Set<string>();
    let current1 = {x:0, y:0};
    let current2 = {x:0, y:0};
    set.add(getId(current1));
    set.add(getId(current2));

    let turn = 0;
    for(const char of input) {
        if (turn % 2 === 0) {
            current1 = update(char, current1);
            set.add(getId(current1));
        } else {
            current2 = update(char, current2);
            set.add(getId(current2));
        }
        turn += 1;
    }
    return set.size.toString();
}
