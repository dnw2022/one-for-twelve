export class ObjectPreJsonSerializer {
  static copy<TSource>(
    from: TSource,
    customize: (from: TSource, to: any) => void
  ): any {
    let src = from as any;
    let dest = {} as any;

    for (let key of Object.keys(src)) {
      if (src[key] !== null) {
        dest[key] = src[key];
      }
    }

    customize(from, dest);

    return dest;
  }
}
