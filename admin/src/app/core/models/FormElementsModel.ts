export class Url {

  constructor(public path: string = '', public id: number = 0) { }

}
export class FilesWithPrev {
  files: File[]
  urls: Url[]
  constructor(files: any = [], urls: any = []) {
    this.files = files;
    this.urls = urls
  }
}

