import {Store,Module,ActionContext} from 'vuex'
import ListModule from './list-module'
import ListState from './list-state'
import Student from '../entities/student'
import Order from '../entities/order'
import Ajax from '../../lib/ajax'
import PageResult from '@/store/entities/page-result';
import ListMutations from './list-mutations'

interface OrderState extends ListState<Order>{
    editOrder:Order
    freeList: Array<Order>
}
class OrderMutations extends ListMutations<Order>{

}
class OrderModule extends ListModule<OrderState,any,Order>{
    state={
        totalCount:0,
        currentPage:1,
        pageSize:10,
        list: new Array<Order>(),
        freeList:new Array<Order>(),
        loading:false,
        editOrder:new Order()
    }
    actions={
        async getAll(context:ActionContext<OrderState,any>,payload:any){
            context.state.loading=true;
            let reponse=await Ajax.get('/api/services/app/Order/GetAll',{params:payload.data});
            context.state.loading=false;
            let page=reponse.data.result as PageResult<Order>;
            context.state.totalCount=page.totalCount;
            context.state.list=page.items;
        },
        async getAllFree(context:ActionContext<OrderState,any>,payload:any){
            context.state.loading=true;
            let reponse=await Ajax.get('/api/services/app/Order/GetAll',{params:payload.data});
            context.state.loading=false;
            let page=reponse.data.result as PageResult<Order>;
            context.state.totalCount=page.totalCount;
            context.state.freeList=page.items;
        },

        async create(context:ActionContext<OrderState,any>,payload:any){
            await Ajax.post('/api/services/app/Order/Create',payload.data);
        },
        async update(context:ActionContext<OrderState,any>,payload:any){
            await Ajax.put('/api/services/app/Order/Update',payload.data);
        },
        async delete(context:ActionContext<OrderState,any>,payload:any){
            await Ajax.delete('/api/services/app/Order/Delete?Id='+payload.data.id);
        },
        async get(context:ActionContext<OrderState,any>,payload:any){
            let reponse=await Ajax.get('/api/services/app/Order/Get?Id='+payload.id);
            return reponse.data.result as Order;
        }
    };
    mutations={
        setCurrentPage(state:OrderState,page:number){
            state.currentPage=page;
        },
        setPageSize(state:OrderState,pagesize:number){
            state.pageSize=pagesize;
        },
        edit(state:OrderState,order:Order){
            state.editOrder=order;
        }
    }
}
const  orderModule=new OrderModule();
export default orderModule;