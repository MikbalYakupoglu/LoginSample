import { ResultModel } from "./resultModel";

export interface SingleResultModel<T> extends ResultModel{
    data : T
}