export function part1(input: string[]): string {
    let total = 0;
    for(const box of input) {
        const match = box.match(/(\d+)x(\d+)x(\d+)/);
        const [,l, w, h] = 
                match?.map((item, index) => index > 0  && index < 4 ? parseInt(item) : null) || [];
        if (l && w && h) {
            const area1 = l*w;
            const area2 = w*h;
            const area3 = h*l;
            const min = Math.min(area1, area2, area3);
            total += 2*area1 + 2*area2 + 2*area3 + min;
        }
    }
    return total.toString();
}