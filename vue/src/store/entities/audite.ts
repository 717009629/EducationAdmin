import Entity from './entity'
import ContractState from './contractState'
export default class Audite {
    contractId: number;
    contractState: ContractState;
    reason: string;
}