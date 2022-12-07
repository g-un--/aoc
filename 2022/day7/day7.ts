type File = { type: "file", name: string, size: number, parent: Dir }

type Dir = { type: "dir", name: string, items: (Dir | File)[], parent: Dir | undefined }

type Session = { CurrentDir: Dir, Root: Dir }

type SessionItem = {
  type: "cd",
  dirName: string
} | {
  type: "ls"
} | {
  type: "dir",
  dirName: string
} | {
  type: "file"
  size: number,
  fileName: string
}

function parseSessionItem(item: string): SessionItem {
  const parts = item.split(" ");
  switch (parts[0]) {
    case "$": {
      if (parts[1] === "cd") {
        return { type: "cd", dirName: parts[2].replace("\r", "") }
      } else {
        return { type: "ls" }
      }
    }
    case "dir": {
      return { type: "dir", dirName: parts[1].replace("\r", "") }
    }
    default: {
      return { type: "file", size: Number.parseInt(parts[0].replace("\r", ""), 10), fileName: parts[1].replace("\r", "") }
    }
  }
}

function buildSession(sessionItems: SessionItem[]) {
  const rootDir: Dir = { type: "dir", name: "/", items: [], parent: undefined };
  const session: Session = { CurrentDir: rootDir, Root: rootDir };

  return sessionItems.reduce<Session>((current, item) => {
    switch (item.type) {
      case "cd": {
        if (item.dirName === "/") {
          return { CurrentDir: current.Root, Root: current.Root };
        } else if (item.dirName === "..") {
          return { CurrentDir: current.CurrentDir.parent!, Root: current.Root }
        } else {
          var dirFound = <Dir>current.CurrentDir.items.find(dirItem => dirItem.type === "dir" && dirItem.name === item.dirName)!;
          return { CurrentDir: dirFound, Root: current.Root }
        }
      }
      case "ls": {
        return current;
      }
      case "dir": {
        if (!current.CurrentDir.items.find(dirItem => dirItem.type === "dir" && dirItem.name === item.dirName)) {
          current.CurrentDir.items.push({ type: "dir", name: item.dirName, items: [], parent: current.CurrentDir });
        }

        return current;
      }
      case "file": {
        if (!current.CurrentDir.items.find(dirItem => dirItem.type === "file" && dirItem.name === item.fileName)) {
          current.CurrentDir.items.push({ type: "file", name: item.fileName, parent: current.CurrentDir, size: item.size })
        }
      }
      default:
        return current;
    }
  }, session);
}

function getDirPath(dir: Dir) {
  const pathToRoot: string[] = [];
  let current: Dir | undefined = dir;
  while (current && current.name !== "/") {
    pathToRoot.unshift(current.name);
    current = current.parent;
  }
  return `/${pathToRoot.join("/")}`
}

function getDirsSize(rootDir: Dir, result: Map<string, number>) {
  let total = 0;
  var path = getDirPath(rootDir);
  for (const item of rootDir.items) {
    if (item.type === "file") {
      total += item.size;
    } else {
      const path = getDirPath(item);
      getDirsSize(item, result);
      total += result.get(path)!;
    }
  }
  result.set(path, total);
  return result;
}

export function part1(input: string[]): number {
  const sessionItems = input.filter(item => item != "").map(item => parseSessionItem(item));
  const session = buildSession(sessionItems);
  const dirsSize = getDirsSize(session.Root, new Map<string, number>());
  const result = [...dirsSize.values()].filter(size => size <= 100000).reduce((sum, item) => sum + item, 0);
  return result;
}

export function part2(input: string[]): number {
  const sessionItems = input.filter(item => item != "").map(item => parseSessionItem(item));
  const session = buildSession(sessionItems);
  const dirsSize = getDirsSize(session.Root, new Map<string, number>());
  const rootSize = dirsSize.get("/")!;
  const diskSpace = 70000000;
  const unusedSpaceNeeded = 30000000;
  const toRemove = unusedSpaceNeeded - (diskSpace - rootSize);
  const dirsSizeSorted = [...dirsSize.values()].sort((a, b) => a - b);
  const first = dirsSizeSorted.filter(item => item >= toRemove)[0];
  return first;
}
