type PlayerMove = "Rock" | "Paper" | "Scissors"

type GameOutcome = "Win" | "Loss" | "Draw";

const mapOfPointsForMove = new Map<PlayerMove, number>([
  ["Rock", 1],
  ["Paper", 2],
  ["Scissors", 3]
]);

const mapOfPointsForGame = new Map<GameOutcome, number>([
  ["Loss", 0],
  ["Draw", 3],
  ["Win", 6]
]);

function play(playersChoices: PlayerMove[]): GameOutcome | undefined {
  if (playersChoices[0] === playersChoices[1])
    return "Draw";

  if (playersChoices.includes("Rock") && playersChoices.includes("Scissors"))
    return playersChoices.indexOf("Rock") === 1 ? "Win" : "Loss";

  if (playersChoices.includes("Rock") && playersChoices.includes("Paper"))
    return playersChoices.indexOf("Paper") === 1 ? "Win" : "Loss";

  if (playersChoices.includes("Scissors") && playersChoices.includes("Paper"))
    return playersChoices.indexOf("Scissors") === 1 ? "Win" : "Loss";

  return undefined;
}

export function part1(input: string[]): number {
  const mapOfMoves = new Map<string, PlayerMove>([
    ["A", "Rock"],
    ["B", "Paper"],
    ["C", "Scissors"],
    ["X", "Rock"],
    ["Y", "Paper"],
    ["Z", "Scissors"],
  ]);

  const parseGame = (input: string): PlayerMove[] => {
    const moves = input.split(' ');
    const opponentMove = mapOfMoves.get(moves[0])!;
    const yourMove = mapOfMoves.get(moves[1])!;
    return [opponentMove, yourMove];
  }

  return input.reduce((score, moves) => {
    if (moves === '')
      return score;

    const playerMoves = parseGame(moves);

    const yourMove = playerMoves[1];
    const movePoints = mapOfPointsForMove.get(yourMove)!;

    const gameResult = play(playerMoves)!;
    const gamePoints = mapOfPointsForGame.get(gameResult)!;

    return score + gamePoints + movePoints;
  }, 0);
}

export function part2(input: string[]): number {
  const mapOfMoves = new Map<string, PlayerMove>([
    ["A", "Rock"],
    ["B", "Paper"],
    ["C", "Scissors"]
  ]);

  const mapOfResults = new Map<string, GameOutcome>([
    ["X", "Loss"],
    ["Y", "Draw"],
    ["Z", "Win"]
  ]);

  const parseGame = (input: string): { opponentMove: PlayerMove, outcome: GameOutcome } => {
    const moveOutcome = input.split(' ');
    const opponentMove = mapOfMoves.get(moveOutcome[0])!;
    const outcome = mapOfResults.get(moveOutcome[1])!;
    return { opponentMove, outcome };
  }

  return input.reduce((score, moveOutcome) => {
    if (moveOutcome === '')
      return score;

    const { opponentMove, outcome } = parseGame(moveOutcome);

    const possibleMoves: PlayerMove[] = ["Rock", "Paper", "Scissors"];
    const yourMove = possibleMoves.filter((myMove) => play([opponentMove, myMove])! === outcome)[0];
    const movePoints = mapOfPointsForMove.get(yourMove)!;

    const gamePoints = mapOfPointsForGame.get(outcome)!;
    return score + movePoints + gamePoints;
  }, 0);
}
