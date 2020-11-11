local genCode = require(PluginPath..'/GenCode_CSharp')
local genCodeModel = require(PluginPath..'/GenCode_CSharpModel')

function onPublish(handler)
    fprint('Handling gen code in plugin')

    if not handler.genCode then 
        fprint('没有勾选代码生成')
        handler.genCode = false
        genCode(handler)
    else
        fprint('勾选代码生成')
        handler.genCode = false
        genCodeModel(handler)
    end

    -- if not handler.genCode then 
    --     handler.genCode = false --prevent default output
    --     fprint('Handling gen code in plugin')
    --     genCode(handler) --do it myself
    -- end
end

function onDestroy()
-------do cleanup here-------
end