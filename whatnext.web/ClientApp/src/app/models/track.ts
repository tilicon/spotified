import { Artist } from "./artist";

export class Track {
  id: string;
  isSelected: boolean;
  name: string;
  type: string;
  artist: Artist;
}
