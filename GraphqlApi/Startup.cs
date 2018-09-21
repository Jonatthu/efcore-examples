using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Server.Transports.AspNetCore;
using GraphQL.Server.Transports.WebSockets;
using GraphQL.Server.Ui.GraphiQL;
using GraphQL.Server.Ui.Playground;
using GraphQL.Server.Ui.Voyager;
using GraphqlApi.GraphSchema;
using GraphqlApi.GraphSchema.GraphTypes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GraphqlApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			services.AddSingleton<IChat, Chat>();
			services.AddSingleton<ChatSchema>();
			services.AddSingleton<ChatQuery>();
			services.AddSingleton<ChatMutation>();
			services.AddSingleton<ChatSubscriptions>();
			services.AddSingleton<MessageType>();
			services.AddSingleton<MessageInputType>();

			// http
			services.AddGraphQLHttp();

			// subscriptions
			services.Configure<ExecutionOptions<ChatSchema>>(options =>
			{
				options.EnableMetrics = true;
				options.ExposeExceptions = true;
			});

			services.AddSingleton<ITokenListener, TokenListener>();
			services.AddSingleton<IPostConfigureOptions<ExecutionOptions<ChatSchema>>, AddAuthenticator>();

			// register default services for web socket. This will also add the protocol handler.
			services.AddGraphQLWebSocket<ChatSchema>();

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);


			//services.AddSingleton<IUserService, MyCustomUserService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			//app.UseGenTyNETProject();




			app.UseDefaultFiles();

			app.UseStaticFiles();

			app.UseWebSockets();

			app.UseGraphQLWebSocket<ChatSchema>(new GraphQLWebSocketsOptions {
				Path = "/graphql"
			});

			app.UseGraphQLHttp<ChatSchema>(new GraphQLHttpOptions());

			app.UseGraphQLPlayground(new GraphQLPlaygroundOptions()
			{
				Path = "/ui/playground"
			});

			app.UseGraphiQLServer(new GraphiQLOptions
			{
				GraphiQLPath = "/ui/graphiql",
				GraphQLEndPoint = "/graphql"
			});

			app.UseGraphQLVoyager(new GraphQLVoyagerOptions()
			{
				GraphQLEndPoint = "/graphql",
				Path = "/ui/voyager"
			});

			app.UseMvc();
		}
    }
}
