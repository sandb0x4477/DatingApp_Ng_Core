import { Photo } from './photo';

export interface User {
  id: number;
  username: string;
  gender: string;
  age: number;
  knownAs: string;
  created: Date | string;
  lastActive: Date | string;
  city: string;
  country: string;
  photoUrl: string;
  interest?: string;
  introduction?: string;
  lookingFor?: string;
  photos?: Photo[];
}
