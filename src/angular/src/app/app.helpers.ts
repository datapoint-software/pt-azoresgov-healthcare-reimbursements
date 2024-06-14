export const isNumber = (subject: unknown): subject is number =>
  "number" === typeof subject;
