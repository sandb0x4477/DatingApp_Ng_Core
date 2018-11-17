export interface Photo {
  id: number;
  url: string;
  description: string;
  dateAdded: Date | string;
  isMain: boolean;
}
