export const isNumber = (subject: unknown): subject is number =>
  "number" === typeof subject;

export const isSameObject = (a: object, b: object) =>
  JSON.stringify(a) === JSON.stringify(b);
