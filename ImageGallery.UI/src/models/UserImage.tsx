import { Comment } from "./Comment";

export interface UserImage {
  id?: number;
  title?: String;
  imagePath?: String;
  likesCount?: number;
  dislikesCount?: number;
  views?: number;
  username?: String;
  comments?: Comment[];
  createTime?: Date;
  lastEditTime?: Date;
}
