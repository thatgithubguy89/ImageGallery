import { FilterRequest } from "./FilterRequest";
import { PageRequest } from "./PageRequest";

export interface QueryRequest {
  filter?: FilterRequest;
  page?: PageRequest;
}
