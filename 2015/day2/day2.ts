function getDimensions(box: string) : {l: number, w: number, h: number} {
    const match = box.match(/(\d+)x(\d+)x(\d+)/);
    const [,l, w, h] = 
            match?.map((item, index) => index > 0  && index < 4 ? parseInt(item) : null) || [];
 
    if (l && w && h) {
        return {l, w, h};
    }

    return {l:0, w:0, h:0};
}

export function part1(input: string[]): string {
    let total = 0;
    for(const box of input) {
        const {l, w, h} = getDimensions(box);
        const area1 = l*w;
        const area2 = w*h;
        const area3 = h*l;
        const min = Math.min(area1, area2, area3);
        total += 2*area1 + 2*area2 + 2*area3 + min;
    }
    return total.toString();
}

export function part2(input: string[]): string {
    let total = 0;
    for(const box of input) {
        const {l, w, h} = getDimensions(box);
        const perimeter =  2 * Math.min(l+w, w+h, h+l);
        const bow = l*w*h;
        total += perimeter + bow;
    }
    return total.toString();
}