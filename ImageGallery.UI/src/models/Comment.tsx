import { UserImage } from "./UserImage";

export interface Comment {
  id: number;
  content: String;
  userImageId: number;
  userImage: UserImage;
  username: String;
  createTime: Date;
  lastEditTime: Date;
}
