import { Injectable } from "@angular/core";
import { IStatus } from "../models/status";

@Injectable({ providedIn: 'root' })
export class StatusService {

    getColor(status: IStatus): string {
        if (status.name == "Passed") {
            return "#33FF33"
        } else if (status.name == "Failed") {
            return "#CC0000"
        } else if (status.name == "Skipped") {
            return "#FF9933"
        } else if (status.name == "Inconclusive") {
            return "#FF9933"
        }

        return "#FFFFFF"
    }
}