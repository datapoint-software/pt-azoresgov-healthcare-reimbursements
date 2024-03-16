export const isLetter = (c: string)  => {

  if (c.length !== 1)
    return;

  const cc = c.charCodeAt(0);

  return (
    (cc >= ('a'.charCodeAt(0)) && cc <= ('a'.charCodeAt(0))) ||
    (cc >= ('A'.charCodeAt(0)) && cc <= ('Z'.charCodeAt(0)))
  );
};
