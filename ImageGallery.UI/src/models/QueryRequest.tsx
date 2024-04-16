import { FilterRequest } from "./FilterRequest";
import { PageRequest } from "./PageRequest";

export interface QueryRequest<T> {
  filter?: FilterRequest;
  page?: PageRequest<T>;
}
