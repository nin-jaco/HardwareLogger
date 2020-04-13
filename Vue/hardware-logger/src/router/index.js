import Vue from 'vue'
import Router from 'vue-router'
import Hello from '@/components/Hello'
import HardwareLogger from '@/components/HardwareLogger'

Vue.use(Router)

export default new Router({
  routes: [
    {
	  path: '/hardware-logger',
	  name: 'HardwareLogger',
	  component: HardwareLogger,
	  meta: {
		requiresAuth: false
	  }
	},
	{
      path: '/',
      name: 'Hello',
      component: Hello
    }
  ]
})
