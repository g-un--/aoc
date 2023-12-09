from utils import getLines
from functools import cmp_to_key
from collections import Counter

def getHand(line):
    cardsAndBid = line.split(' ')
    bid = int(cardsAndBid[1])
    cards = [c for c in cardsAndBid[0]]
    return (cards, bid)

def groupCards(cards):
    result = [0 for i in range(0,5)]
    counter = Counter(cards)
    for _, count in counter.items():
        result[5 - count] += 1
    return result

def compHandsWithScores(a,b):
    for a1,b1 in zip(a[2],b[2]):
        if a1 == b1:
            continue 
        return a1-b1
    return 0
    
def getCards():
    return {
        "2": 2, "3": 3, "4": 4, "5": 5, "6": 6, "7": 7, "8": 8, "9": 9, "T": 10, "J": 11, "Q": 12, "K": 13, "A": 14 
    }

def getCardsWithJ():
    return {
        "2": 2, "3": 3, "4": 4, "5": 5, "6": 6, "7": 7, "8": 8, "9": 9, "T": 10, "J": 1, "Q": 12, "K": 13, "A": 14 
    }

def getScore(cards):
    groups = groupCards(cards)
    scores = getCards()
    cardScores = list(map(lambda x: scores[x], cards))
    return groups + cardScores

def getScoreWithJ(cards):
    groups = groupCards(filter(lambda x: x != "J", cards))
    js = len(list(filter(lambda x: x == "J", cards)))
    if js > 0:
        allJ = True
        for i, item in enumerate(groups):
            if item > 0:
                groups[i - js] = 1
                groups[i] -= 1
                allJ = False
                break
        if allJ:
            groups[0] = 1
    scores = getCardsWithJ()
    cardScores = list(map(lambda x: scores[x], cards))
    return groups + cardScores

def getTotal(sortedHands):
    score = 0
    for index, hand in enumerate(sortedHands):
        score += hand[1] * (index + 1)
    return score

def part1():
    lines = getLines(__file__)
    hands = list(map(getHand, lines))
    handsWithScores = list(map(lambda x: (*x, getScore(x[0])), hands))
    sortedHands = sorted(handsWithScores, key=cmp_to_key(compHandsWithScores))
    return getTotal(sortedHands)

def part2():
    lines = getLines(__file__)
    hands = list(map(getHand, lines))
    handsWithScores = list(map(lambda x: (*x, getScoreWithJ(x[0])), hands))
    sortedHands = sorted(handsWithScores, key=cmp_to_key(compHandsWithScores))
    return getTotal(sortedHands)
    
def test_part_1():
    assert part1() == 253933213
    
def test_part_2():
    assert part2() == 253473930
